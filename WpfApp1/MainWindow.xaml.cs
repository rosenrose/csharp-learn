using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> Points = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(Grid) switch
            {
                { X: var x, Y: var y } => new(x, y)
            };

            Points.Add(point);

            if (Points.Count < 2)
            {
                return;
            }

            AddLine(Points[^2], Points[^1]);

            if (Points.Count >= 3)
            {
                AddLine(Points[^1], Points[0]);
                Points.Clear();
            }

        }
        private void AddLine(Point p1, Point p2)
        {
            Line line = new()
            {
                Stroke = Brushes.BlueViolet,
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
            };

            Canvas.Children.Add(line);
        }
    }
}
