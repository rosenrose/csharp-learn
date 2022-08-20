using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (Width, Height) = (960, 540);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            (Width, Height) = (640, 640);
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            Button1.Visibility = Button1.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
