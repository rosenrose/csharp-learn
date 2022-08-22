using System.Windows;
using System.Windows.Controls;

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

            RadioButton1.IsChecked = null;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            string msg = $"check1: {(CheckBox1.IsChecked == null ? "indeterminate" : CheckBox1.IsChecked)}, check2: {CheckBox2.IsChecked}";
            MessageBox.Show(msg);
        }

        private void OnCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((CheckBox)sender).IsChecked.ToString());
        }

        private void OnCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((CheckBox)sender).IsChecked.ToString());
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((RadioButton)sender).IsChecked.ToString());
        }

        private void OnRadioButtonUnchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((RadioButton)sender).IsChecked.ToString());
        }
    }
}
