using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Employees Emps;
        public MainWindow()
        {
            InitializeComponent();

            Emps = new();
            DataContext = Emps;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Employee Emp = new(TextBox1.Text, TextBox2.Text);

            if (ListView.SelectedItems.Count == 0)
            {
                Emps.Add(Emp);
                return;
            }

            Emps.Insert(ListView.SelectedIndex, Emp);
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            foreach (Employee Item in ListView.SelectedItems)
            {
                Item.Name = $"{TextBox1.Text} {TextBox2.Text}";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Clear?", "Clear", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                return;
            }

            foreach (Employee Item in ListView.SelectedItems)
            {
                Item.Name = "";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            foreach (Employee Item in ListView.SelectedItems)
            {
                ListView.Items.Remove(Item);
            }
        }

        private void Purge_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            Emps.Clear();
        }
    }

    public class Employees : ObservableCollection<Employee>
    {
        public Employees() : base()
        {
            Add(new("hello", "world"));
            Add(new("foo", "bar"));
            Add(new("Kim", "John"));
            Add(new("김", "철"));
        }
    }
    public class Employee
    {
        public Employee(string first, string last) => (FirstName, LastName) = (first, last);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string? name;
        public string Name
        {
            get => string.IsNullOrEmpty(name) ? $"{FirstName} {LastName}" : name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    (FirstName, LastName, name) = ("", "", "");
                    return;
                }

                name = value;

                string[] names = value.Split(' ');
                (FirstName, LastName) = names.Length == 2 ? (names[0], names[1]) : ("", "");
            }
        }
    }
}