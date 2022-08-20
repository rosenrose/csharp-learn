using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

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

            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            var (Width, Height) = ((int)this.Width, (int)this.Height);

            Random rand = new();
            Rectangle rect = new()
            {
                Width = rand.Next(Width / 2),
                Height = rand.Next(Height / 2),
                Stroke = Brushes.DeepSkyBlue
            };
            Canvas.SetLeft(rect, rand.Next(Width / 2));
            Canvas.SetTop(rect, rand.Next(Height / 2));

            MainCanvas.Children.Add(rect);
        }
    }
}
