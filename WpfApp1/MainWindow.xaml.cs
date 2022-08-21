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
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open");
        }

        private void OptionClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Option");
        }
    }
}
