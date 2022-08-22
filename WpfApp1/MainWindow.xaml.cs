using System;
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
        string[] Items = { "apple", "banana", "grape", "peach" };
        public MainWindow()
        {
            InitializeComponent();

            label.Content = Items[0];
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

        private void ButtonSpinner_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;
            Label label = (Label)spinner.Content;
            string? value = label.Content.ToString();

            int index = string.IsNullOrEmpty(value) ? 0 : Array.IndexOf(Items, value);

            if (e.Direction == SpinDirection.Increase)
            {
                index = index > 0 ? index - 1 : index;
            }
            else
            {
                index = index < Items.Length - 1 ? index + 1 : index;
            }

            label.Content = Items[index];
        }
    }
}
