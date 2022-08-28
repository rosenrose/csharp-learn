using MySql.Data.MySqlClient;
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
        private string dbName;
        public string DbName
        {
            get => dbName;
            set
            {
                if (value != dbName)
                {
                    dbName = value;
                    RaisePropertyChanged(nameof(DbName));
                }
            }
        }
        private string id;
        public string Id
        {
            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    RaisePropertyChanged(nameof(Id));
                }
            }
        }
        private string Password;

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
            if (Conn == null || Conn.State != System.Data.ConnectionState.Closed)
            {
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
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (Conn == null || Conn.State == System.Data.ConnectionState.Closed)
            {
                return;
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
                return;
            }

            try
            {
                Conn.Dispose();
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
    }
}