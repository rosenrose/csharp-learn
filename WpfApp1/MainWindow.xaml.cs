using System.Windows;
using System.Windows.Input;

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
            //Button Button1 = (Button)sender;
            Button1.Content = "Hello";
            MessageBox.Show("Hi");
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show($"{e.Key} | {e.KeyStates} | {e.SystemKey} | {e.ImeProcessedKey} | {Keyboard.Modifiers}");
            if (e.Key == Key.A && Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                MessageBox.Show("a");
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            MessageBox.Show($"{e.InputSource} | {e.Source} | {e.Device} | {e.KeyboardDevice}");
        }
    }
}
