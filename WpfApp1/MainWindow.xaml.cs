using System;
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
        Point InitPoint, EndPoint;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            InitPoint = e.GetPosition(Grid) switch
            {
                { X: var x, Y: var y } => new(x, y)
            };

            AddRectangle(InitPoint, InitPoint);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            if (MainCanvas.Children.Count == 0)
            {
                return;
            }

            EndPoint = e.GetPosition(Grid) switch
            {
                { X: var x, Y: var y } => new(x, y)
            };

            Rectangle rect = (Rectangle)MainCanvas.Children[^1];
            rect.Width = Math.Abs(InitPoint.X - EndPoint.X);
            rect.Height = Math.Abs(InitPoint.Y - EndPoint.Y);

            if (EndPoint.X < InitPoint.X)
            {
                Canvas.SetLeft(rect, EndPoint.X);
            }
            if (EndPoint.Y < InitPoint.Y)
            {
                Canvas.SetTop(rect, EndPoint.Y);
            }
        }

        private void AddRectangle(Point p1, Point p2)
        {
            Rectangle rect = new()
            {
                Stroke = Brushes.BlueViolet,
                Width = Math.Abs(p1.X - p2.X),
                Height = Math.Abs(p1.Y - p2.Y),
            };

            MainCanvas.Children.Add(rect);
            Canvas.SetLeft(rect, p1.X);
            Canvas.SetTop(rect, p1.Y);
        }
    }
}
