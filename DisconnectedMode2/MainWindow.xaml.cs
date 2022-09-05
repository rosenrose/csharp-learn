using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        private MySqlDataAdapter NameAdapter;
        private MySqlDataAdapter FruitAdapter;
        private Visibility visibility = Visibility.Hidden;
        public Visibility InputVisibility
        {
            get => visibility;
            set
            {
                if (value != visibility)
                {
                    visibility = value;
                    RaisePropertyChanged(nameof(InputVisibility));
                }
            }
        }
        private IEnumerable<object> ids;
        public IEnumerable<object> Ids
        {
            get => ids;
            set
            {
                if (value != ids)
                {
                    ids = value;
                    RaisePropertyChanged(nameof(Ids));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void SqlCommandInit()
        {
            NameAdapter = new("SELECT * FROM name;", Conn);
            FruitAdapter = new("SELECT * FROM fruit;", Conn);
            MySqlCommandBuilder NameBuilder = new(NameAdapter);
            MySqlCommandBuilder FruitBuilder = new(FruitAdapter);
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            Conn = new($"Server=localhost;Port=3306;Database={DbName};Uid={Id};Pwd={Password};");
            SqlCommandInit();

            MySqlDataAdapter DataAdapter = new("SELECT * FROM name; SELECT * FROM fruit;", Conn);
            NameFruitSet.Clear();
            DataAdapter.Fill(NameFruitSet);

            (NameTable, FruitTable) = (NameFruitSet.Tables[0], NameFruitSet.Tables[1]);
            NameTable.RowChanged += (object sender, DataRowChangeEventArgs e) => UpdateIds();

            NameFruitSet.Relations.Clear();
            NameFruitSet.Relations.Add(new("NameFruitRelation", NameTable.Columns["Id"]!, FruitTable.Columns["Id"]!));

            ConnectionState = "Data Fetched";
            InputVisibility = Visibility.Visible;
            UpdateIds();
        }

        private void UpdateIds()
        {
            Ids = NameTable.Rows.Cast<DataRow>().Select(row => row["Id"]);
        }

        private DataRow AddRow(DataTable table, object?[] items)
        {
            DataRow row = table.NewRow();
            row.ItemArray = items;
            table.Rows.Add(row);

            return row;
        }

        private void NameInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddRow(NameTable, new object?[] { Name_Id.Text, Name.Text });
                NameAdapter.Update(NameTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void NameUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Name.SelectedItems.Count == 0)
            {
                return;
            }

            DataRow row = ((DataRowView)DataGrid_Name.SelectedItem).Row;
            object?[] SavedItemArray = row.ItemArray;

            try
            {
                row.ItemArray = new object?[] { Name_Id.Text, Name.Text };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                NameAdapter.Update(NameTable);
            }
            catch (Exception ex)
            {
                string NewId = Name_Id.Text;
                row.ItemArray = SavedItemArray;

                if (ex is MySqlException && ex.Message.Contains("a foreign key constraint fails"))
                {
                    AddRow(NameTable, new object?[] { NewId, Name.Text });
                    NameAdapter.Update(NameTable);

                    DataRow[] FruitRows = row.GetChildRows("NameFruitRelation");
                    foreach (DataRow Row in FruitRows)
                    {
                        AddRow(FruitTable, new object?[] { NewId, Row["Fruit"] });
                    }

                    DeleteNameRow(row);
                    FruitAdapter.Update(FruitTable);
                    NameAdapter.Update(NameTable);

                    return;
                }

                MessageBox.Show(ex.ToString());
            }
        }

        private void DeleteNameRow(DataRow row)
        {
            DataRow[] FruitRows = row.GetChildRows("NameFruitRelation");

            foreach (DataRow Row in FruitRows)
            {
                Row.Delete();
            }

            row.Delete();
        }

        private void NameDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Name.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            while (DataGrid_Name.SelectedItems.Count > 0)
            {
                DataRow NameRow = ((DataRowView)DataGrid_Name.SelectedItems[0]!).Row;
                DeleteNameRow(NameRow);
            }

            FruitAdapter.Update(FruitTable);    //종속관계를 먼저 업데이트
            NameAdapter.Update(NameTable);
        }
        private void NameClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Clear?", "Clear", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                while (NameTable.Rows.Count > 0)
                {
                    DeleteNameRow(NameTable.Rows[0]);
                    FruitAdapter.Update(FruitTable);
                    NameAdapter.Update(NameTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FruitInsert_Click(object sender, RoutedEventArgs e)
        {
            DataRow? AddedRow = null;

            try
            {
                AddedRow = AddRow(FruitTable, new object?[] { Fruit_Id.Text, Fruit.Text });
                FruitAdapter.Update(FruitTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                if (AddedRow == null)
                {
                    return;
                }
                FruitTable.Rows.Remove(AddedRow!);
            }
        }
        private void FruitUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Fruit.SelectedItems.Count == 0)
            {
                return;
            }

            DataRow row = ((DataRowView)DataGrid_Fruit.SelectedItem).Row;
            object?[] SavedItemArray = row.ItemArray;

            try
            {
                row.ItemArray = new object?[] { Fruit_Id.Text, Fruit.Text };
                FruitAdapter.Update(FruitTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                row.ItemArray = SavedItemArray;
            }
        }
        private void FruitDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Fruit.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            while (DataGrid_Fruit.SelectedItems.Count > 0)
            {
                ((DataRowView)DataGrid_Fruit.SelectedItems[0]!).Row.Delete();
            }

            FruitAdapter.Update(FruitTable);
        }
        private void FruitClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Clear?", "Clear", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                while (FruitTable.Rows.Count > 0)
                {
                    FruitTable.Rows[0].Delete();
                    FruitAdapter.Update(FruitTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
