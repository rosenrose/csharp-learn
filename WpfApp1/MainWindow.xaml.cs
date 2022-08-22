using System.Linq;
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
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            MessageBox.Show($"{comboBox.SelectedIndex} {comboBox.Text} {comboBox.SelectedItem}");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            MessageBox.Show($"{listBox.SelectedIndex} {listBox.SelectedItem}");
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            MessageBox.Show(string.Join('\n', listBox.SelectedItems.Cast<ListBoxItem>().Select(i => i.Content)));
        }
    }
}
