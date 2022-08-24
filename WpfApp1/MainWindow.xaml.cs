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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"index: {TabControl.SelectedIndex}\nitem: {TabControl.SelectedItem}\n" +
                $"value: {TabControl.SelectedValue}\ncontent: {TabControl.SelectedContent}");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"index: {TabControl.SelectedIndex}\nitem: {TabControl.SelectedItem}\n" +
                $"value: {TabControl.SelectedValue}\ncontent: {TabControl.SelectedContent}");
        }
    }
}