namespace Smart_Cooker
{
    internal class Program
    {
        public static MenuItems MainMenuIndex = MenuItems.Power;
        public const int MenuItemPadding = 10;

        public const int WindowWidth = 140;
        public const int WindowHeight = 45;

        public const int RiceCookerX = 0;
        public const int RiceCookerY = 0;
        public const int RiceCookerWidth = WindowWidth / 2;
        public const int RiceCookerHeight = 25;

        public const int RiceX = RiceCookerX + RiceCookerWidth;
        public const int RiceY = RiceCookerY;
        public const int RiceWidth = RiceCookerWidth / 2;
        public const int RiceHeight = RiceCookerHeight;

        public const int WaterX = RiceX + RiceWidth;
        public const int WaterY = RiceY;
        public const int WaterWidth = RiceWidth;
        public const int WaterHeight = RiceHeight;

        public const int InfoX = RiceCookerX;
        public const int InfoY = RiceCookerHeight;
        public const int InfoWidth = RiceCookerWidth;
        public const int InfoHeight = 15;

        public const int MenuX = InfoX + InfoWidth;
        public const int MenuY = InfoY;
        public const int MenuWidth = RiceCookerWidth;
        public const int MenuHeight = 15;

        public const int RiceCookerBodyX = RiceCookerX + 25;
        public const int RiceCookerBodyY = RiceCookerY + 13;
        public const int RiceCookerBodyWidth = 30;
        public const int RiceCookerBodyHeight = 10;

        public const int RiceCookerCoverX = RiceCookerBodyX;
        public const int RiceCookerCoverY = RiceCookerBodyY - 2;
        public const int RiceCookerCoverWidth = RiceCookerBodyWidth;
        public const int RiceCookerCoverHeight = 2;

        public const int PowerLineWidth = 10;

        public const int MessageBoxX = MenuX + (MenuWidth - MessageBoxWidth) / 2;
        public const int MessageBoxY = MenuY + (MenuHeight - MessageBoxHeight) / 2;
        public const int MessageBoxWidth = MenuWidth / 2;
        public const int MessageBoxHeight = 3;

        public struct RiceCookerInfo
        {
            public int Rice;
            public int Water;
            public bool IsCoverOpen = false;
            public bool Power = false;
            public int Count = 0;
            public CookerProcess State = CookerProcess.None;
            public RiceCookerInfo(int Rice_, int Water_) => (Rice, Water) = (Rice_, Water_);
        }
        public enum MenuItems { Power, Cover, Cook, Warm, Cancel, Count, Rice, Water, Quit }
        public enum CookerProcess { None, Ricing, Watering, Washing, Flushing, Cooking, Done, Warming }

        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize(WindowWidth, WindowHeight);
            RiceCookerInfo RCInfo = new(10 * 1000, 5 * 1000);
            string[] MenuItemsKor = { "전원", "뚜껑", "취사", "보온", "취소", "인원수", "쌀", "물", "종료" };

            //Test();
            DrawBox(RiceCookerX, RiceCookerY, RiceCookerWidth, RiceCookerHeight, "스마트 밥솥");
            DrawBox(RiceX, RiceY, RiceWidth, RiceHeight, "쌀통");
            DrawBox(WaterX, WaterY, WaterWidth, WaterHeight, "물통");
            DrawBox(InfoX, InfoY, InfoWidth, InfoHeight, "밥솥 정보");
            DrawBox(MenuX, MenuY, MenuWidth, MenuHeight, "메뉴");
            DrawBox(RiceCookerBodyX, RiceCookerBodyY, RiceCookerBodyWidth, RiceCookerBodyHeight, "밥솥");
            DrawRiceCookerCover(RiceCookerCoverX, RiceCookerCoverY, RiceCookerCoverWidth, RiceCookerCoverHeight, RCInfo.IsCoverOpen);
            DrawRiceCookerInfo(RCInfo);
            DrawPowerLine(RCInfo.Power);
            DrawRice(RCInfo.Rice);
            DrawWater(RCInfo.Water);

            do
            {
                DrawMenu(MenuX + ((InfoWidth - MenuItemPadding - 1) / 2), MenuY + 4, MenuItemsKor);

                switch (MainMenuIndex)
                {
                    case MenuItems.Power:
                        RCInfo.Power = !RCInfo.Power;
                        DrawPowerLine(RCInfo.Power);
                        break;
                    case MenuItems.Cover:
                        RCInfo.IsCoverOpen = !RCInfo.IsCoverOpen;
                        DrawRiceCookerCover(RiceCookerCoverX, RiceCookerCoverY, RiceCookerCoverWidth, RiceCookerCoverHeight, RCInfo.IsCoverOpen);
                        break;
                    case MenuItems.Cook:
                        DrawMessageBox("zzzz");
                        break;
                    case MenuItems.Warm:
                        break;
                    case MenuItems.Cancel:
                        break;
                    case MenuItems.Count:
                        break;
                    case MenuItems.Rice:
                        break;
                    case MenuItems.Water:
                        break;
                }

                DrawRiceCookerInfo(RCInfo);
            } while (MainMenuIndex != MenuItems.Quit);

            Console.SetCursorPosition(0, WindowHeight - 2);
        }

        private static void DrawMenu(int x, int y, string[] MenuItems)
        {
            ConsoleKeyInfo InputKey;
            ConsoleKey[] QuitKeys = { ConsoleKey.Enter, ConsoleKey.Escape };

            do
            {
                foreach (var (MenuItem, i) in MenuItems.Select((v, i) => (v, i)))
                {
                    if ((int)MainMenuIndex == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(x, y + i);
                    Console.Write(CenteredString(MenuItem, MenuItemPadding));

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                InputKey = Console.ReadKey(true);

                switch (InputKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        MainMenuIndex = (MenuItems)(((int)MainMenuIndex + MenuItems.Length - 1) % MenuItems.Length);
                        break;
                    case ConsoleKey.DownArrow:
                        MainMenuIndex = (MenuItems)(((int)MainMenuIndex + 1) % MenuItems.Length);
                        break;
                    case ConsoleKey.Escape:
                        MainMenuIndex = (MenuItems)(MenuItems.Length - 1);
                        break;
                }
            } while (!QuitKeys.Contains(InputKey.Key));
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

        static void DrawBox(int x, int y, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(i switch
                {
                    0 => $"┌{new string('─', width - 2)}┐",
                    int j when j == height - 1 => $"└{new string('─', width - 2)}┘",
                    _ => $"│{new string(' ', width - 2)}│"
                });
            }
        }
        static void DrawBox(int x, int y, int width, int height, string title)
        {
            DrawBox(x, y, width, height);
            if (title.Length > width / 2)
            {
                Console.SetCursorPosition(x + 1, y + 1);
            }
            else
            {
                Console.SetCursorPosition(x + width / 4, y + 1);
            }
            Console.Write(CenteredString(title, width / 2));
        }
        static void ClearBox(int x, int y, int width, int height)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write($"{new string(' ', width)}");
            }
        }

        static void DrawRice(int Amount_)
        {
            int Amount = Amount_ / 1000;

            ClearBox(RiceX + 1, RiceY + 2, RiceWidth - 2, RiceHeight - 3);

            for (int i = 0; i < Amount; i++)
            {
                Console.SetCursorPosition(RiceX + 1, RiceY + RiceHeight - 2 - i);
                Console.Write(new string('⊙', RiceWidth - 2));
            }
        }
        static void DrawWater(int Amount_)
        {
            int Amount = Amount_ / 1000;
            Console.BackgroundColor = ConsoleColor.Blue;

            ClearBox(WaterX + 1, WaterY + 2, WaterWidth - 2, WaterHeight - 3);

            for (int i = 0; i < Amount; i++)
            {
                Console.SetCursorPosition(WaterX + 1, WaterY + WaterHeight - 2 - i);
                Console.Write(new string(' ', WaterWidth - 2));
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void DrawRiceCookerCover(int x, int y, int width, int height, bool IsCoverOpen)
        {
            int width_ = width / 3;
            ClearBox(x, y - width_ + RiceCookerCoverHeight, width, width_);

            if (IsCoverOpen)
            {
                DrawBox(x, y - width_ + RiceCookerCoverHeight, height, width_);
            }
            else
            {
                DrawBox(x, y, width, height);
            }
        }
        static void DrawRiceCookerInfo(RiceCookerInfo RCInfo)
        {
            Dictionary<string, string> Infos = new()
            {
                ["전원 상태"] = RCInfo.Power ? "켜짐" : "꺼짐",
                ["뚜껑 상태"] = RCInfo.IsCoverOpen ? "열림" : "닫힘",
                ["밥솥 상태"] = RCInfo.State switch
                {
                    CookerProcess.None => "대기 중",
                    CookerProcess.Ricing => "밥 넣기",
                    CookerProcess.Watering => "물 넣기",
                    CookerProcess.Washing => "쌀 씻기",
                    CookerProcess.Flushing => "물 배수",
                    CookerProcess.Cooking => "취사 중",
                    CookerProcess.Done => "취사 완료",
                    CookerProcess.Warming => "보온 중",
                    _ => "오류"
                },
                ["인원수"] = RCInfo.Count.ToString(),
                ["쌀 상태"] = $"{RCInfo.Rice / 1000.0f:f1} kg",
                ["물 상태"] = $"{RCInfo.Water / 1000.0f:f1} 리터"
            };

            ClearBox(InfoX + 1, InfoY + 2, InfoWidth - 2, InfoHeight - 3);
            foreach (var (key, value, i) in Infos.Select((KeyVal, i) => (KeyVal.Key, KeyVal.Value, i)))
            {
                Console.SetCursorPosition(InfoX + 3, InfoY + 2 + ((i + 1) * 2) - 1);
                Console.Write($"{key}: {value}");
            }
        }
        static void DrawPowerLine(bool Power)
        {
            if (Power)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }

            Console.SetCursorPosition(RiceCookerBodyX - PowerLineWidth, RiceCookerBodyY + RiceCookerBodyHeight - 2);
            Console.Write(new string('─', PowerLineWidth));

            Console.BackgroundColor = ConsoleColor.Black;
        }
        static string DrawMessageBox(string message)
        {
            DrawBox(MessageBoxX, MessageBoxY, MessageBoxWidth, MessageBoxHeight, message);
            string? Input = Console.ReadLine();
            ClearBox(MessageBoxX, MessageBoxY, MessageBoxWidth, MessageBoxHeight);

            return Input ?? "";
        }

        static void Test()
        {
            // Draw and Clear Test
            DrawBox(0, 0, 10, 5);
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("dd");
            Console.SetCursorPosition(10 - 3, 3);
            Console.WriteLine("dd");
            ClearBox(1, 1, 8, 3);
            Console.SetCursorPosition(0, 6);
        }
    }
}