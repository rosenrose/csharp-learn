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

        private void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "C# (*.cs)|*.cs|XAML (*.xaml)|*.xaml"
            };

            dialog.ShowDialog();
        }

        private void OnOpenFolderClick(object sender, RoutedEventArgs e)
        {
            new System.Windows.Forms.FolderBrowserDialog().ShowDialog();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            new Microsoft.Win32.SaveFileDialog().ShowDialog();
        }
    }
}