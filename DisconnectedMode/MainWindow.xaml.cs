using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DisconnectedMode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public enum Gender { Male = 0, Female = 1 }
        public DataTable StudentTable { get; set; } = new("Student");

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            InitializeTable();
        }

        private void InitializeTable()
        {
            DataColumn NameCol = new("Name", typeof(string))
            {
                MaxLength = 20,
                AllowDBNull = false
            };

            StudentTable.Columns.Add(NameCol);
            StudentTable.Columns.Add(new DataColumn("Age", typeof(int)));
            StudentTable.Columns.Add(new DataColumn("Gender", typeof(string)));

            DataColumn TimestampCol = new("create_time", typeof(DateTime))
            {
                AllowDBNull = false
            };
            StudentTable.Columns.Add(TimestampCol);
            StudentTable.PrimaryKey = new[] { TimestampCol };

            DataRow StudentRow = StudentTable.NewRow();
            StudentRow.ItemArray = new object[] { "Hello", 17, Gender.Male, DateTime.Now };
            StudentTable.Rows.Add(StudentRow);

            StudentRow = StudentTable.NewRow();
            StudentRow.ItemArray = new object[] { "World", 23, Gender.Female, DateTime.Now };
            StudentTable.Rows.Add(StudentRow);

            StudentRow = StudentTable.NewRow();
            StudentRow.ItemArray = new object[] { "foo", 19, null, DateTime.Now };
            StudentTable.Rows.Add(StudentRow);

            StudentRow = StudentTable.NewRow();
            StudentRow.ItemArray = new object[] { "bar", null, null, DateTime.Now };
            StudentTable.Rows.Add(StudentRow);

            //MessageBox.Show(Table1.Select("Age > 20")[0]["Name"].ToString());
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = StudentTable.NewRow();
            row.ItemArray = new object[]
            {
                TextBox_Name.Text,
                UpDown_Age.Value,
                (Gender)Convert.ToUInt32(RadioButton_Female.IsChecked),
                DateTime.Now
            };

            StudentTable.Rows.Add(row);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 0)
            {
                return;
            }

            DataRow row = ((DataRowView)DataGrid.SelectedItem).Row;
            row.ItemArray = new object[]
            {
                TextBox_Name.Text,
                UpDown_Age.Value,
                (Gender)Convert.ToUInt32(RadioButton_Female.IsChecked),
                row["create_time"]
            };
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

            StudentTable.Rows.Remove(((DataRowView)DataGrid.SelectedItem).Row);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Clear?", "Clear", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            StudentTable.Rows.Clear();
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
