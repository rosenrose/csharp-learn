using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private ObservableCollection<Employee> emps = new();
        public ObservableCollection<Employee> Employees
        {
            get => emps;
            set
            {
                if (emps != value)
                {
                    emps = value;
                    RaisePropertyChanged("Employees");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Employees.Add(new("hello", "world"));
            Employees.Add(new("foo", "bar"));
            Employees.Add(new("Kim", "John"));
            Employees.Add(new("김", "철"));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Employee Emp = new(TextBox1.Text, TextBox2.Text);

            if (ListView.SelectedItems.Count == 0)
            {
                Employees.Add(Emp);
                return;
            }

            Employees.Insert(ListView.SelectedIndex, Emp);
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }

            Employees = new(Employees.Select(emp => ListView.SelectedItems.Contains(emp) ? new(TextBox1.Text, TextBox2.Text) : emp));
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

            Employees = new(Employees.Select(emp => ListView.SelectedItems.Contains(emp) ? new("", "") : emp));
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

            Employees = new(Employees.Where(emp => !ListView.SelectedItems.Contains(emp)));
        }

        private void Purge_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.Items.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            Employees.Clear();
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