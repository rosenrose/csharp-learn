namespace Smart_Cooker
{
    internal class Program
    {
        public static int MainMenuIndex = 0;

        public const int RiceCookerX = 0;
        public const int RiceCookerY = 0;
        public const int RiceCookerWidth = 10;
        public const int RiceCookerHeight = 20;

        public const int RiceOrWaterX = 10;
        public const int RiceOrWaterY = 10;
        public const int RiceOrWaterWidth = 5;
        public const int RiceOrWaterHeight = 20;

        public const int InfoOrMenuX = 20;
        public const int InfoOrMenuY = 20;
        public const int InfoOrMenuWidth = 10;
        public const int InfoOrMenuHeight = 10;

        public const int RiceCookerBodyWidth = 0;
        public const int RiceCookerBodyHeight = 0;
        public const int RiceCookerCoverWidth = 0;
        public const int RiceCookerCoverHeight = 0;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize(140, 40);
            RiceCookerInfo RCInfo = new(10 * 1000, 5 * 1000);
            string[] MenuItems = { "전원", "뚜껑", "취사", "보온", "취소", "인원수", "쌀", "물", "종료" };

            Test();
            //Menu(65, 5, MenuItems);
        }

        public struct RiceCookerInfo
        {
            private int Rice;
            private int Water;
            public RiceCookerInfo(int Rice_, int Water_) => (Rice, Water) = (Rice_, Water_);
        }

        private static void Menu(int x, int y, string[] MenuItems)
        {
            ConsoleKeyInfo InputKey;
            while (true)
            {
                foreach (var (MenuItem, i) in MenuItems.Select((v, i) => (v, i)))
                {
                    if (MainMenuIndex == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(x, y + i);
                    Console.Write(CenteredString(MenuItem, 10));

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                InputKey = Console.ReadKey(true);

                switch (InputKey.Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.Escape:
                        break;
                    case ConsoleKey.UpArrow:
                        MainMenuIndex = (MainMenuIndex + 1) % MenuItems.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        MainMenuIndex = (MainMenuIndex + MenuItems.Length - 1) % MenuItems.Length;
                        break;
                }
            }
        }

        static string CenteredString(string str, int width)
        {
            int Length = str.Length;
            if (Length >= width)
            {
                return str;
            }

            int leftPadding = (width - Length) / 2;

            return str.PadLeft(leftPadding + Length).PadRight(width);
        }

        static void DrawBox(int x, int y, int width_, int height)
        {
            int width = width_ * 2;
            Console.SetCursorPosition(x, y);

            for (int i = 0; i < height; i++)
            {
                Console.WriteLine(i switch
                {
                    0 => $"┌{"".PadLeft(width - 2, '─')}┐",
                    int j when j == height - 1 => $"└{"".PadLeft(width - 2, '─')}┘",
                    _ => $"│{"".PadLeft(width - 2)}│"
                });
            }
        }
        static void ClearBox(int x, int y, int width_, int height)
        {
            int width = width_ * 2;
            Console.SetCursorPosition(x, y + 1);
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < height - 2; i++)
            {
                Console.WriteLine($"│{"".PadLeft(width - 2)}");
            }
        }

        static void DrawRiceCookerBox(int x, int y)
        {
            DrawBox(x, y, RiceCookerWidth, RiceCookerHeight);
        }
        static void DrawRiceOrWaterBox(int x, int y)
        {
            DrawBox(x, y, RiceOrWaterWidth, RiceOrWaterHeight);
        }
        static void DrawRice(int x, int y, int Amount_)
        {
            int Amount = Amount_ / 1000;
            Console.WriteLine("".PadLeft(20, '⊙'));
        }
        static void DrawWater(int x, int y, int Amount_)
        {
            int Amount = Amount_ / 1000;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("".PadLeft(20));
        }
        static void DrawInfoOrMenu(int x, int y)
        {
            DrawBox(x, y, InfoOrMenuWidth, InfoOrMenuHeight);
        }
        static void DrawRiceCookerBody(int x, int y)
        {
            DrawBox(x, y, RiceCookerBodyWidth, RiceCookerBodyHeight);
        }
        static void DrawRiceCookerCover(int x, int y)
        {
            DrawBox(x, y, RiceCookerCoverWidth, RiceCookerCoverHeight);
        }

        static void Test()
        {
            // Draw and Clear Test
            DrawBox(0, 0, 5, 5);
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("dd");
            Console.SetCursorPosition(10 - 3, 3);
            Console.WriteLine("dd");
            ClearBox(0, 0, 5, 5);
            Console.SetCursorPosition(0, 6);
        }
    }
}