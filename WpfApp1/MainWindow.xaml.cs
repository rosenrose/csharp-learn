using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(Grid) switch
            {
                { X: var x, Y: var y } => new(x, y)
            };

            AddCircle(point, 100, 100);
        }

        private void AddCircle(Point point, int width, int height)
        {
            Ellipse circle = new()
            {
                Stroke = Brushes.Aqua,
                Width = width,
                Height = height,
            };

            MainCanvas.Children.Add(circle);
            Canvas.SetLeft(circle, point.X - width / 2);
            Canvas.SetTop(circle, point.Y - height / 2);
        }
    }
}
