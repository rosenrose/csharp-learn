namespace ConsoleApp1
{
    internal class Program
    {
        class Date
        {
            private int Year;
            private int month;
            public int Month
            {
                get => month * 2;
                set => month = value - 1;
            }
            public int Day { get; set; } = 20;
            public int Hour { get; }
            public int Minute;
            public void Print()
            {
                Console.WriteLine($"{Year} {Month} {Day}");
            }
            public Date(int day, int min) => Day = day;
        }
        static void Main(string[] args)
        {
            Date d = new(min: 10, day: 15);
            //Console.WriteLine($"{d.Year}");
            d.Month = 9;
            Console.WriteLine($"{d.Month} {d.Day}");
            //d.Hour = 10;
        }
    }
}