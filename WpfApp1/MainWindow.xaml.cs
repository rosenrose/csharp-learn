using System.ComponentModel;
using System.Windows;

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

        private string msg = "world";
        public string Message
        {
            get => msg;
            set
            {
                if (msg != value)
                {
                    msg = value;
                    RaisePropertyChanged("Message");
                }
            }
        }
        private Window1? Modeless = null;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Window1 Modal = new() { Owner = this, IsModal = true };

            Message = $"{Modal.ShowDialog()} {Modal.a + Modal.b}";
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (Modeless != null)
            {
                return;
            }

            Modeless = new() { Owner = this, IsModal = false };
            Modeless.Closing += (object sender, CancelEventArgs e) =>
            {
                Message = $"{Modeless.DialogResult} {Modeless.a + Modeless.b}";
                Modeless = null;
            };

            Modeless.Show();
        }
    }
}