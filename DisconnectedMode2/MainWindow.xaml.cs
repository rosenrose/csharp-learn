using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace DisconnectedMode2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataSet NameFruitSet { get; set; } = new("NameFruitSet");
        private DataTable NameTable = new("Name");
        private DataTable FruitTable = new("Fruit");

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            InitializeDataSet();
        }

        private void InitializeDataSet()
        {
            NameFruitSet.Tables.Add(NameTable);
            NameFruitSet.Tables.Add(FruitTable);

            DataColumn IdCol = new("Id", typeof(string))
            {
                MaxLength = 20,
                Unique = true
            };

            NameTable.Columns.Add(IdCol);
            NameTable.PrimaryKey = new[] { IdCol };
            NameTable.Columns.Add(new DataColumn("Password", typeof(string)) { MaxLength = 50 });
            NameTable.Columns.Add(new DataColumn("Name", typeof(string)) { MaxLength = 30 });

            AddRow(NameTable, new object[] { "111", "abc", "hello" });
            AddRow(NameTable, new object[] { "223", "ghx", "world" });
            AddRow(NameTable, new object[] { "foo", "1", "zzz" });
            AddRow(NameTable, new object[] { "bar", "2", "ㅋㅋㅋ" });
            AddRow(NameTable, new object[] { "empty", "", "" });

            IdCol = new("Id", typeof(string)) { MaxLength = 20 };
            DataColumn FruitCol = new("Fruit", typeof(string)) { MaxLength = 20 };

            FruitTable.Columns.Add(IdCol);
            FruitTable.Columns.Add(FruitCol);
            FruitTable.PrimaryKey = new[] { IdCol, FruitCol };
            FruitTable.Constraints.Add(
                new ForeignKeyConstraint("NameFK", NameTable.Columns["id"]!, FruitTable.Columns["id"]!)
            );

            AddRow(FruitTable, new object?[] { "111", "apple" });
            AddRow(FruitTable, new object?[] { "223", "peach" });
            AddRow(FruitTable, new object?[] { "223", "grape" });
            AddRow(FruitTable, new object?[] { "foo", "grape" });
            AddRow(FruitTable, new object?[] { "foo", "banana" });
            AddRow(FruitTable, new object?[] { "bar", "apple" });
            AddRow(FruitTable, new object?[] { "foo", "orange" });
            AddRow(FruitTable, new object?[] { "bar", "strawberry" });

            NameFruitSet.Relations.Add(new("NameFruitRelation", NameTable.Columns["id"]!, FruitTable.Columns["id"]!));
        }

        private void AddRow(DataTable table, object?[] items)
        {
            DataRow Row = table.NewRow();
            Row.ItemArray = items;
            table.Rows.Add(Row);
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

    public class FruitToName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

            DataRow FruitRow = ((DataRowView)value).Row;
            DataRow NameRow = FruitRow.GetParentRow("NameFruitRelation")!;

            return NameRow["Name"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
