using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DisconnectedMode2
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

        private DataSet StudentFruitSet = new("StudentFruitSet");
        public DataTable StudentTable { get; set; } = new("StudentTable");
        public DataTable FruitTable { get; set; } = new("FruitTable");
        public string DbName { get; set; } = "school";
        public string Id { get; set; } = "root";
        private string Password;
        private MySqlConnection? Conn = null;
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
        public enum Gender { Male = 0, Female = 1 }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = $"Server=localhost;Port=3306;Database={DbName};Uid={Id};Pwd={Password};";

            if (Conn != null)
            {
                Conn.Dispose();
            }

            Conn = new(ConnectionString);

            //MySqlDataAdapter DataAdapter = new("SELECT * FROM student; SELECT * FROM fruit;", Conn);
            //DataAdapter.Fill(StudentFruitSet);
            //(StudentTable, FruitTable) = (StudentFruitSet.Tables[0], StudentFruitSet.Tables[1]);

            StudentFruitSet.Tables.Add(StudentTable);
            StudentFruitSet.Tables.Add(FruitTable);

            MySqlDataAdapter DataAdapter = new("SELECT * FROM student;", Conn);
            DataAdapter.Fill(StudentTable);
            DataAdapter = new("SELECT * FROM fruit;", Conn);
            DataAdapter.Fill(FruitTable);

            StudentFruitSet.Relations.Add(new("NameFruitRelation", StudentTable.Columns["Name"]!, FruitTable.Columns["Name"]!));

            ConnectionState = "Data Fetched";
        }

        private void SetData_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class NameToFruit : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // x:Name 사용 불가능
            if (value == null)
            {
                return "";
            }

            DataRow NameRow = ((DataRowView)value).Row;
            DataRow[] FruitRows = NameRow.GetChildRows("NameFruitRelation");

            return FruitRows.Select(row => row["fruit"]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class FruitToAge : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

            DataRow FruitRow = ((DataRowView)value).Row;
            DataRow NameRow = FruitRow.GetParentRow("NameFruitRelation")!;

            return NameRow!["Age"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class GenderToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((MainWindow.Gender)System.Convert.ToUInt32(value)).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
