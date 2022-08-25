using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string? propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public int a, b;
        private Regex regex = new(@"\D+");
        private string? msg;
        public string? Message
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
        public bool IsModal;

        public Window1()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            txtBox.Text = regex.Replace(txtBox.Text, "");
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (!IsModal)
            {
                return;
            }

            DialogResult = true;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Message = ((MainWindow)Owner).Message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            a = int.TryParse(TextBox1.Text, out a) ? a : 0;
            b = int.TryParse(TextBox2.Text, out b) ? b : 0;

            Close();
        }
    }
}
