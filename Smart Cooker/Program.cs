namespace Smart_Cooker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            //Console.WriteLine($"{Console.BufferHeight} {Console.BufferWidth}");
            //Console.WriteLine($"{Console.BackgroundColor} {Console.ForegroundColor}");
            //Console.WriteLine($"{Console.CursorLeft} {Console.CursorTop} {Console.CursorSize}");
            //Console.WriteLine($"{Console.Title} {Console.WindowWidth} {Console.WindowHeight} {Console.WindowLeft} {Console.WindowTop}");
            //Console.SetWindowSize(100, 15);
            //Console.WriteLine($"{Console.WindowWidth} {Console.WindowHeight}");

            Random rand = new();
            ConsoleColor[] Colors =
            {
                ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Gray,
                ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.Yellow,
                ConsoleColor.Black, ConsoleColor.White
            };
            ConsoleColor[] DarkColors =
            {
                ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkGray,
                ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow
            };

            foreach (var Color in Colors[^2..])
            {
                Console.ForegroundColor = Color;
                Console.WriteLine(Color);
            }
            foreach (var (DarkColor, i) in DarkColors.Select((v, i) => (v, i)))
            {
                Console.ForegroundColor = Colors[i];
                Console.Write($"{Colors[i]} ");
                Console.ForegroundColor = DarkColor;
                Console.WriteLine(DarkColor);
            }
            //Console.BackgroundColor = Colors[0];
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = Colors[rand.Next(Colors.Length)];
                Console.SetCursorPosition(rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight));
                Console.Write("Hello");
                Thread.Sleep(100);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = DarkColors[rand.Next(DarkColors.Length)];
                Console.SetCursorPosition(rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight));
                Console.Write("Hello");
                Thread.Sleep(100);
            }
        }
    }
}