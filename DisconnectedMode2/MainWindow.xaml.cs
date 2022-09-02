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

        private DataSet NameFruitSet = new("NameFruitSet");
        public DataTable nTable = new("NameTable");
        public DataTable NameTable
        {
            get => nTable;
            set
            {
                if (value != nTable)
                {
                    nTable = value;
                    RaisePropertyChanged(nameof(NameTable));
                }
            }
        }
        public DataTable fTable = new("FruitTable");
        public DataTable FruitTable
        {
            get => fTable;
            set
            {
                if (value != fTable)
                {
                    fTable = value;
                    RaisePropertyChanged(nameof(FruitTable));
                }
            }
        }
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

            MySqlDataAdapter DataAdapter = new("SELECT * FROM name; SELECT * FROM fruit;", Conn);
            NameFruitSet.Clear();
            DataAdapter.Fill(NameFruitSet);
            (NameTable, FruitTable) = (NameFruitSet.Tables[0], NameFruitSet.Tables[1]);

            NameFruitSet.Relations.Clear();
            NameFruitSet.Relations.Add(new("NameFruitRelation", NameTable.Columns["Id"]!, FruitTable.Columns["Id"]!));

            ConnectionState = "Data Fetched";
        }

        private void SetData_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class IdToFruit : IValueConverter
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

    public class IdToName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

            DataRow FruitRow = ((DataRowView)value).Row;
            DataRow NameRow = FruitRow.GetParentRow("NameFruitRelation")!;

            return NameRow!["Name"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
