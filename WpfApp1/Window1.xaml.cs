using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;

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

        private Regex regex = new(@"\D");
        public int a, b;
        private string text1, text2;
        public string Text1
        {
            get => text1;
            set
            {
                if (text1 != (value = regex.Replace(value, "")))
                {
                    text1 = value;
                    RaisePropertyChanged("Message");
                }
            }
        }
        public string Text2
        {
            get => text2;
            set
            {
                if (text2 != (value = regex.Replace(value, "")))
                {
                    text2 = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

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

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (!IsModal)
            {
                return;
            }

            DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (a, b) = (int.Parse(Text1), int.Parse(Text2));

            Close();
        }
    }
}
