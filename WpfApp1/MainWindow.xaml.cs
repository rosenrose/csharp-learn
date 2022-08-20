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

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Load");
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Closing", "Close?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void OnClose(object sender, System.EventArgs e)
        {
            MessageBox.Show("Closed");
        }
    }
}
