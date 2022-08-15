using System.Drawing;
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

            TextBox.Text = $"R: {Color.Coral.R} G: {Color.Coral.G} B: {Color.Coral.B} A: {Color.Coral.A}";
        }
    }
}
