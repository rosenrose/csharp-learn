using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

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

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label1.Content = $"x: {e.NewValue}";
        }

        private void ScrollBar_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label2.Content = $"x: {e.NewValue}";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Label3.Content = e.NewValue;
        }

        private void Updown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Label3.Content = e.NewValue;
        }

        private void ButtonSpinner_Spin(object sender, SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;
            ComboBox comboBox = (ComboBox)spinner.Content;

            int index = comboBox.SelectedIndex;

            if (e.Direction == SpinDirection.Increase)
            {
                index = index > 0 ? index - 1 : index;
            }
            else
            {
                index = index < comboBox.Items.Count - 1 ? index + 1 : index;
            }

            comboBox.SelectedIndex = index;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Label3.Content = $"{comboBox.SelectedIndex}, text: {comboBox.Text}, item: {comboBox.SelectedItem}";
        }
    }
}
