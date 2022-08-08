using System.IO;
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
            File.CreateText("test.txt");
        }

        private void OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show("Down");
            File.AppendAllText("test.txt", "MouseDown\n");
        }

        private void OnMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //MessageBox.Show("Up");
            File.AppendAllText("test.txt", "MouseUp\n");
        }

        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBlock.Text = $"{e.GetPosition(Grid).X:F2} {e.GetPosition(Grid).Y:F2}";
        }

        private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            File.AppendAllText("test.txt", "MouseLeftButtonDown\n");
        }

        private void OnMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            File.AppendAllText("test.txt", "MouseLeftButtonUp\n");
        }

        private void OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            File.AppendAllText("test.txt", "MouseDoubleClick\n");
        }
    }
}
