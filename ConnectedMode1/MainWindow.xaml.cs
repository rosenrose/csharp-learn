using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ConnectedMode1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string? propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        private string connState;
        public string ConnectionState
        {
            get => connState;
            set
            {
                if (value != connState)
                {
                    connState = value;
                    RaisePropertyChanged(nameof(ConnectionState));
                }
            }
        }
        private Visibility listVisibility = Visibility.Hidden;
        public Visibility ListVisibility
        {
            get => listVisibility;
            set
            {
                if (value != listVisibility)
                {
                    listVisibility = value;
                    RaisePropertyChanged(nameof(ListVisibility));
                }
            }
        }
        public string DbName { get; set; } = "school";
        public string Id { get; set; } = "root";
        private string Password;
        public DataTable Students { get; set; } = new("Student");
        public string Sql { get; set; }
        private MySqlConnection? Conn = null;
        public enum Gender { Male = 0, Female = 1 }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Students.Columns.Add(new DataColumn("Name", typeof(string))
            {
                MaxLength = 20,
                AllowDBNull = false
            });
            Students.Columns.Add(new DataColumn("Age", typeof(int)));
            Students.Columns.Add(new DataColumn("Gender", typeof(string)));

            DataColumn TimestampCol = new("create_time", typeof(DateTime))
            {
                AllowDBNull = false,
                Unique = true
            };
            Students.Columns.Add(TimestampCol);
            Students.PrimaryKey = new[] { TimestampCol };
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = $"Server=localhost;Port=3306;Database={DbName};Uid={Id};Pwd={Password};";

            try
            {
                if (Conn != null)
                {
                    Conn.Dispose();
                    Students.Clear();
                    ListVisibility = Visibility.Hidden;
                }

                Conn = new(ConnectionString);
                ConnectionState = "Connected";
            }
            catch
            {
                ConnectionState = "Connect Error";
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (Conn == null)
            {
                ConnectionState = "Not Connected";
                return;
            }
            if (Conn.State != System.Data.ConnectionState.Closed)
            {
                ConnectionState = "Already Opened";
                return;
            }

            try
            {
                Conn.Open();
                ConnectionState = Conn.State.ToString();
                ListVisibility = Visibility.Visible;

                RunQuery("SELECT * from student");
            }
            catch
            {
                ConnectionState = "Open Error";
            }
        }

        private void RunQuery(string query)
        {
            MySqlCommand cmd = new(query, Conn);

            try
            {
                using MySqlDataReader Reader = cmd.ExecuteReader();

                Students.Clear();

                while (Reader.Read())
                {
                    DataRow row = Students.NewRow();
                    row.ItemArray = new object?[]
                    {
                        Reader[0],
                        Reader["Age"] == DBNull.Value ? null : Reader["Age"],
                        (Gender)Convert.ToUInt32(Reader["Gender"]),
                        Reader["create_time"]
                    };
                    Students.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (Conn == null)
            {
                ConnectionState = "Not Connected";
                return;
            }

            try
            {
                Conn.Dispose();
                Students.Clear();
                ListVisibility = Visibility.Hidden;

                Conn = null;
                ConnectionState = "Disconnected";
            }
            catch
            {
                ConnectionState = "Disconnect Error";
            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            MySqlCommand cmd = new($"INSERT INTO student VALUES (@name, @age, @gender, @time);", Conn);

            string Name = TextBox_Name.Text;
            int? Age = UpDown_Age.Value;
            Gender gender = (Gender)Convert.ToUInt32(RadioButton_Female.IsChecked);
            DateTime CreateTime = DateTime.Now;

            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@age", Age);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@time", CreateTime);
            //cmd.Parameters.Add(new("@gender", MySqlDbType.Bit) { Value = gender });
            //cmd.Parameters.Add(new("@time", MySqlDbType.Timestamp) { Value = CreateTime });
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                RunQuery("SELECT * from student");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }

            MySqlCommand cmd = new($"UPDATE student SET Name=@name, Age=@age, Gender=@gender WHERE create_time=@time;", Conn);

            string Name = TextBox_Name.Text;
            int? Age = UpDown_Age.Value;
            Gender gender = (Gender)Convert.ToUInt32(RadioButton_Female.IsChecked);
            DateTime CreateTime = (DateTime)((DataRowView)ListView.SelectedItem).Row["create_time"];

            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@age", Age);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@time", CreateTime);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                RunQuery("SELECT * from student");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }


            try
            {
                while (ListView.SelectedItems.Count > 0)
                {
                    MySqlCommand cmd = new($"DELETE FROM student WHERE create_time=@time;", Conn);
                    DataRow SelectedItem = ((DataRowView)ListView.SelectedItems[0]!).Row;
                    DateTime CreateTime = (DateTime)SelectedItem["create_time"];

                    cmd.Parameters.AddWithValue("@time", CreateTime);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    Students.Rows.Remove(SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Clear?", "Delete", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            MySqlCommand cmd = new($"DELETE FROM student;", Conn);
            try
            {
                cmd.ExecuteNonQuery();
                Students.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                UpDown_Age.Value = null;
                return;
            }

            var Age = ((DataRowView)ListView.SelectedItem).Row["Age"];

            UpDown_Age.Value = Age == DBNull.Value ? null : (int?)Age;
        }

        private void SqlRun_Click(object sender, RoutedEventArgs e)
        {
            RunQuery(Sql);
        }
    }

    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == DBNull.Value ? false : (string)value == (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}