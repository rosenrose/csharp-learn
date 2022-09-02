using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DisconnectedMode1
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

        public enum Gender { Male = 0, Female = 1 }
        private DataSet dataSet = new("DataSet");
        public DataTable StudentTable { get; set; } = new("StudentTable");
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
        public string DbName { get; set; } = "school";
        public string Id { get; set; } = "root";
        private string Password;
        private MySqlConnection? Conn = null;
        private MySqlDataAdapter DataAdapter = new();
        public string SearchName { get; set; }
        private Visibility visibility = Visibility.Hidden;
        public Visibility GridVisibility
        {
            get => visibility;
            set
            {
                if (value != visibility)
                {
                    visibility = value;
                    RaisePropertyChanged(nameof(GridVisibility));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            SqlCommandInit();
        }

        private void SqlCommandInit()
        {
            MySqlCommand cmd = new("SELECT * FROM student;", Conn);
            DataAdapter.SelectCommand = cmd;

            cmd = new("INSERT INTO student (Name, Age, Gender) VALUES (@name, @age, @gender);", Conn);
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 20, "Name");
            cmd.Parameters.Add("@age", MySqlDbType.Int32, 0, "Age");
            cmd.Parameters.Add("@gender", MySqlDbType.Bit, 0, "Gender");
            DataAdapter.InsertCommand = cmd;

            cmd = new("UPDATE student SET Name=@name, Age=@age, Gender=@gender WHERE create_time=@time;", Conn);
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 20, "Name");
            cmd.Parameters.Add("@age", MySqlDbType.Int32, 0, "Age");
            cmd.Parameters.Add("@gender", MySqlDbType.Bit, 0, "Gender");
            cmd.Parameters.Add("@time", MySqlDbType.Timestamp, 0, "create_time");
            DataAdapter.UpdateCommand = cmd;

            cmd = new("DELETE FROM student WHERE create_time=@time;", Conn);
            cmd.Parameters.Add("@time", MySqlDbType.Timestamp, 0, "create_time");
            DataAdapter.DeleteCommand = cmd;
        }

        private void Receive_Click(object sender, RoutedEventArgs e)
        {
            Conn = new($"Server=localhost;Port=3306;Database={DbName};Uid={Id};Pwd={Password};");
            SqlCommandInit();

            //dataSet.Clear();
            StudentTable.Clear();
            DataAdapter.Fill(StudentTable);

            ConnectionState = "Data Fetched";
            GridVisibility = Visibility.Visible;
        }

        private void AddRow(DataTable table, object?[] items)
        {
            DataRow row = table.NewRow();
            row.ItemArray = items;
            table.Rows.Add(row);
        }

        private void ModifyTable()
        {
            try
            {
                DataAdapter.Update(StudentTable);
                StudentTable.Clear();
                DataAdapter.Fill(StudentTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            AddRow(StudentTable, new object?[]
            {
                TextBox_Name.Text,
                UpDown_Age.Value,
                RadioButton_Female.IsChecked
            });

            ModifyTable();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 0)
            {
                return;
            }

            DataRow row = ((DataRowView)DataGrid.SelectedItem).Row;
            row.ItemArray = new object?[]
            {
                TextBox_Name.Text,
                UpDown_Age.Value,
                RadioButton_Female.IsChecked,
                row["create_time"]
            };

            ModifyTable();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            while (DataGrid.SelectedItems.Count > 0)
            {
                ((DataRowView)DataGrid.SelectedItems[0]!).Row.Delete();
            }

            ModifyTable();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Clear?", "Clear", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                while (StudentTable.Rows.Count > 0)
                {
                    StudentTable.Rows[0].Delete();
                    DataAdapter.Update(StudentTable);   // Update 해야 Rows.Count 변함
                }

                StudentTable.Clear();
                DataAdapter.Fill(StudentTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 0)
            {
                UpDown_Age.Value = null;
                return;
            }

            var Age = ((DataRowView)DataGrid.SelectedItem).Row["Age"];

            UpDown_Age.Value = Age == DBNull.Value ? null : (int?)Age;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (Conn == null)
            {
                return;
            }

            using MySqlCommand cmd = new("SELECT * from student WHERE name = @name;", Conn);
            cmd.Parameters.AddWithValue("@name", SearchName);
            DataAdapter.SelectCommand = cmd;

            StudentTable.Clear();
            if (DataAdapter.Fill(StudentTable) == 0)
            {
                MessageBox.Show("No Result");
            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
        }

        public static string GenderToString(object value)
        {
            return ((Gender)Convert.ToUInt32(value)).ToString();
        }
    }

    public class GenderToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MainWindow.GenderToString(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == DBNull.Value ? false : MainWindow.GenderToString(value) == (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
