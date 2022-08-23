using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string? propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public double progress;
        public double Progress
        {
            get => progress;
            set
            {
                if (progress != value)
                {
                    progress = value;
                    RaisePropertyChanged("Progress");
                }
            }
        }
        public ObservableCollection<string> SelectedDates { get; set; } = new();

        private DispatcherTimer timer = new()
        {
            Interval = TimeSpan.FromSeconds(0.01)
        };

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            timer.Tick += TimerTick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                return;
            }

            timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            if (Progress >= 100)
            {
                timer.Stop();
                Progress = 0;
                return;
            }

            Progress++;
        }

        private void OnSelectedDatesChanged1(object sender, SelectionChangedEventArgs e)
        {
            ListBox1.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
        }

        private void OnSelectedDatesChanged2(object sender, SelectionChangedEventArgs e)
        {
            SelectedDates.Clear();

            foreach (DateTime dateTime in Calendar2.SelectedDates)
            {
                SelectedDates.Add(dateTime.ToShortDateString());
            }

            //SelDates = new(Calendar2.SelectedDates.Select(dateTime => dateTime.ToShortDateString()));
            //RaisePropertyChanged("SelDates");
        }
        //public List<string> SelDates { get; set; }
    }

    public class DateTimeConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object param, CultureInfo culture)
        {
            string Date, Time;
            Date = value[0] == null ? "" : ((DateTime)value[0]).ToLongDateString();
            Time = value[1] == null ? "" : ((DateTime)value[1]).ToShortTimeString();

            return $"{Date} / {Time}";
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object param, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CalendarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object param, CultureInfo culture) =>
            ((SelectedDatesCollection)value).Select(dateTime => dateTime.ToLongDateString());

        public object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
