using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

    public class SliderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.TryParse((string)parameter, out double multiplier) ? (double)value * multiplier : 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
