using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<Student> Students { get; set; } = new();

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
            }
            catch
            {
                ConnectionState = "Open Error";
            }

            MySqlCommand cmd = new("SELECT * from student", Conn);
            using MySqlDataReader Reader = cmd.ExecuteReader();

            while (Reader.Read())
            {
                Students.Add(new()
                {
                    Name = (string)Reader[0],
                    Age = Reader["Age"] == DBNull.Value ? null : (int)Reader["Age"],
                    Gender = (Student.GenderEnum)Convert.ToInt32(Reader[2])
                });
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (Conn == null)
            {
                ConnectionState = "Not Connected";
                return;
            }
            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                ConnectionState = "Already Closed";
            }

            try
            {
                Conn.Close();
                ConnectionState = Conn.State.ToString();
            }
            catch
            {
                ConnectionState = "Close Error";
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
                Conn = null;

                ConnectionState = "Disconnected";
                Students.Clear();
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
    }
}