using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1
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
        public ObservableCollection<Student> Students { get; set; } = new();
        private MySqlConnection? Conn = null;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
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

                SelectStudent();
            }
            catch
            {
                ConnectionState = "Open Error";
            }
        }

        private void SelectStudent()
        {
            MySqlCommand cmd = new("SELECT * from student", Conn);
            using MySqlDataReader Reader = cmd.ExecuteReader();

            Students.Clear();

            while (Reader.Read())
            {
                Students.Add(new()
                {
                    Name = (string)Reader[0],
                    Age = Reader["Age"] == DBNull.Value ? null : (int)Reader["Age"],
                    Gender = (Student.GenderEnum)Convert.ToUInt32(Reader["Gender"]),
                    CreateTime = (DateTime)Reader["create_time"]
                });
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
            Student.GenderEnum Gender = (Student.GenderEnum)Convert.ToUInt32(RadioButton_Female.IsChecked);
            DateTime CreateTime = DateTime.Now;

            cmd.Parameters.Add(new("@name", MySqlDbType.VarChar, 20) { Value = Name });
            cmd.Parameters.Add(new("@age", MySqlDbType.Int32) { Value = Age });
            cmd.Parameters.Add(new("@gender", MySqlDbType.Bit) { Value = Gender });
            cmd.Parameters.Add(new("@time", MySqlDbType.Timestamp) { Value = CreateTime });
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                SelectStudent();
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
            Student.GenderEnum Gender = (Student.GenderEnum)Convert.ToUInt32(RadioButton_Female.IsChecked);
            DateTime CreateTime = ((Student)ListView.SelectedItem).CreateTime;

            cmd.Parameters.Add(new("@name", MySqlDbType.VarChar, 20) { Value = Name });
            cmd.Parameters.Add(new("@age", MySqlDbType.Int32) { Value = Age });
            cmd.Parameters.Add(new("@gender", MySqlDbType.Bit) { Value = Gender });
            cmd.Parameters.Add(new("@time", MySqlDbType.Timestamp) { Value = CreateTime });
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                SelectStudent();
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

            MySqlCommand cmd = new($"DELETE FROM student WHERE create_time=@time;", Conn);
            Student SelectedItem = (Student)ListView.SelectedItem;
            DateTime CreateTime = SelectedItem.CreateTime;

            cmd.Parameters.Add(new("@time", MySqlDbType.Timestamp) { Value = CreateTime });
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                Students.Remove(SelectedItem);
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
            UpDown_Age.Value = ListView.SelectedItems.Count > 0 ? ((Student)ListView.SelectedItem).Age : null;
        }
    }

    public class Student
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value ?? "";
        }
        public int? Age { get; set; }
        public enum GenderEnum { Male = 0, Female = 1 }
        public GenderEnum Gender { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Student.GenderEnum)value).ToString() == (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}