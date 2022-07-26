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

        public const int MenuItemsX = MenuX + ((InfoWidth - MenuItemPadding - 1) / 2);
        public const int MenuItemsY = MenuY + 4;

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
        public const int MessageBoxWidth = MenuWidth / 2 + 10;
        public const int MessageBoxHeight = 3;

        public const int RicePerPerson = 160;
        public const int WaterPerPerson = 170;
        public const int RiceMax = 20 * 1000;
        public const int WaterMax = 22 * 1000;

        public struct RiceCookerInfo
        {
            public int Rice;
            public int Water;
            public bool IsCoverOpen = false;
            public bool Power = false;
            public bool isEmpty = true;
            public int Count = 0;
            public CookerProcess State = CookerProcess.None;
            public RiceCookerInfo(int Rice_, int Water_) => (Rice, Water) = (Rice_, Water_);
        }
        public enum MenuItems { Power, Cover, Cook, Warm, Eat, Count, Rice, Water, Quit }
        public enum CookerProcess { None, Ricing, Watering, Washing, Flushing, Cooking, Done, Warming }

        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize(WindowWidth, WindowHeight);
            RiceCookerInfo RCInfo = new(5 * 1000, 10 * 1000);
            string[] MenuItemsKor = { "전원", "뚜껑", "취사", "보온", "밥먹기", "인원수", "쌀", "물", "종료" };

            //Test();
            DrawOutFrame(RCInfo);

            Thread CookThread = new((RCInfo_) =>
            {
                RCInfo = Cook(RCInfo_);
                DrawOutFrame(RCInfo);
                DrawMenuItems(MenuItemsKor);
            });

            do
            {
                DrawMenu(MenuItemsKor);

                switch (MainMenuIndex)
                {
                    case MenuItems.Power:
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중입니다.");
                            break;
                        }

                        RCInfo.Power = !RCInfo.Power;
                        if (RCInfo.State == CookerProcess.Warming)
                        {
                            DrawRiceCookerBodyRicing("밥솥");
                        }

                        RCInfo.State = CookerProcess.None;
                        DrawPowerLine(RCInfo.Power);
                        break;
                    case MenuItems.Cover:
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중에는 뚜껑을 열 수 없습니다.");
                            break;
                        }
                        if (RCInfo.State == CookerProcess.Warming)
                        {
                            DrawMessageBox("보온중에는 뚜껑을 열 수 없습니다.");
                            break;
                        }

                        RCInfo.IsCoverOpen = !RCInfo.IsCoverOpen;
                        DrawRiceCookerCover(RCInfo.IsCoverOpen);
                        break;
                    case MenuItems.Cook:
                        if (!RCInfo.Power)
                        {
                            DrawMessageBox("전원이 꺼져 있습니다.");
                            break;
                        }
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중입니다.");
                            break;
                        }
                        if (!RCInfo.isEmpty)
                        {
                            DrawMessageBox("밥이 있습니다.");
                            break;
                        }
                        if (RCInfo.Count == 0)
                        {
                            DrawMessageBox("인원수를 입력해 주세요.");
                            break;
                        }

                        RCInfo.State = CookerProcess.Ricing;
                        CookThread.Start(RCInfo);
                        CookThread = new((RCInfo_) =>
                        {
                            RCInfo = Cook(RCInfo_);
                            DrawOutFrame(RCInfo);
                            DrawMenuItems(MenuItemsKor);
                        });
                        break;
                    case MenuItems.Warm:
                        if (!RCInfo.Power)
                        {
                            DrawMessageBox("전원이 꺼져 있습니다.");
                            break;
                        }
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중입니다.");
                            break;
                        }
                        if (RCInfo.IsCoverOpen)
                        {
                            DrawMessageBox("뚜껑이 열려 있습니다.");
                            break;
                        }
                        if (RCInfo.isEmpty)
                        {
                            DrawMessageBox("밥이 없습니다.");
                            break;
                        }

                        if (RCInfo.State == CookerProcess.Warming)
                        {
                            RCInfo.State = CookerProcess.None;
                            DrawRiceCookerBodyRicing("대기 중");
                        }
                        else
                        {
                            RCInfo.State = CookerProcess.Warming;
                            DrawRiceCookerBodyWarming("보온");
                        }
                        break;
                    case MenuItems.Eat:
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중입니다.");
                            break;
                        }
                        if (!RCInfo.IsCoverOpen)
                        {
                            DrawMessageBox("뚜껑이 닫혀 있습니다.");
                            break;
                        }
                        if (RCInfo.isEmpty)
                        {
                            DrawMessageBox("밥이 없습니다.");
                            break;
                        }

                        RCInfo.isEmpty = true;
                        DrawRiceCookerBody("밥솥");
                        break;
                    case MenuItems.Count:
                        if (!RCInfo.Power)
                        {
                            DrawMessageBox("전원이 꺼져 있습니다.");
                            break;
                        }
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중입니다.");
                            break;
                        }

                        int Count;
                        if (int.TryParse(DrawMessageBox("인원수 입력: "), out Count))
                        {
                            RCInfo.Count = Count;
                        }
                        break;
                    case MenuItems.Rice:
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중입니다.");
                            break;
                        }

                        int Rice;
                        if (int.TryParse(DrawMessageBox("추가할 쌀(kg): "), out Rice))
                        {
                            RCInfo.Rice += Math.Min(Rice * 1000, RiceMax - RCInfo.Rice);
                            DrawRice(RCInfo.Rice);
                        }
                        break;
                    case MenuItems.Water:
                        if (CookThread.IsAlive)
                        {
                            DrawMessageBox("취사중입니다.");
                            break;
                        }

                        int Water;
                        if (int.TryParse(DrawMessageBox("추가할 물(리터): "), out Water))
                        {
                            RCInfo.Water += Math.Min(Water * 1000, WaterMax - RCInfo.Water);
                            DrawWater(RCInfo.Water);
                        }
                        break;
                }

                DrawRiceCookerInfo(RCInfo);
            } while (MainMenuIndex != MenuItems.Quit);

            Console.SetCursorPosition(0, WindowHeight - 2);
        }

        static void DrawMenu(string[] MenuItems)
        {
            ConsoleKeyInfo InputKey;
            ConsoleKey[] QuitKeys = { ConsoleKey.Enter, ConsoleKey.Escape };

            do
            {
                DrawMenuItems(MenuItems);

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
        static void DrawMenuItems(string[] MenuItems)
        {
            foreach (var (MenuItem, i) in MenuItems.Select((v, i) => (v, i)))
            {
                if ((int)MainMenuIndex == i)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(MenuItemsX, MenuItemsY + i);
                Console.Write(CenteredString(MenuItem, MenuItemPadding));

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
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
        static int ConsoleLength(string str)
        {
            return str.Select(c => c < 128 ? 1 : (('ㄱ' <= c && c <= 'ㅎ') || ('가' <= c && c <= '힣')) ? 2 : 1).Sum();
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
            if (ConsoleLength(title) > width / 2)
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

        static void DrawOutFrame(RiceCookerInfo RCInfo)
        {
            DrawBox(RiceCookerX, RiceCookerY, RiceCookerWidth, RiceCookerHeight, "스마트 밥솥");
            if (RCInfo.isEmpty)
            {
                DrawRiceCookerBody("밥솥");
            }
            else
            {
                if (RCInfo.State == CookerProcess.Warming)
                {
                    DrawRiceCookerBodyWarming("보온");
                }
                else
                {
                    DrawRiceCookerBodyRicing("대기 중");
                }
            }
            DrawRiceCookerCover(RCInfo.IsCoverOpen);
            DrawPowerLine(RCInfo.Power);

            DrawBox(RiceX, RiceY, RiceWidth, RiceHeight, "쌀통");
            DrawBox(WaterX, WaterY, WaterWidth, WaterHeight, "물통");
            DrawRice(RCInfo.Rice);
            DrawWater(RCInfo.Water);

            DrawBox(InfoX, InfoY, InfoWidth, InfoHeight, "밥솥 정보");
            DrawRiceCookerInfo(RCInfo);
            DrawBox(MenuX, MenuY, MenuWidth, MenuHeight, "메뉴");
        }
        static void DrawRice(int Amount_)
        {
            ClearBox(RiceX + 1, RiceY + 2, RiceWidth - 2, RiceHeight - 3);

            int Amount = Amount_ / 1000;
            for (int i = 0; i < Amount; i++)
            {
                Console.SetCursorPosition(RiceX + 1, RiceY + RiceHeight - 2 - i);
                Console.Write(new string('⊙', RiceWidth - 2));
            }
        }
        static void DrawWater(int Amount_)
        {
            ClearBox(WaterX + 1, WaterY + 2, WaterWidth - 2, WaterHeight - 3);
            Console.BackgroundColor = ConsoleColor.Blue;

            int Amount = Amount_ / 1000;
            for (int i = 0; i < Amount; i++)
            {
                Console.SetCursorPosition(WaterX + 1, WaterY + WaterHeight - 2 - i);
                Console.Write(new string(' ', WaterWidth - 2));
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void DrawRiceCookerBody(string title)
        {
            DrawBox(RiceCookerBodyX, RiceCookerBodyY, RiceCookerBodyWidth, RiceCookerBodyHeight, title);
        }
        static void DrawRiceCookerCover(bool IsCoverOpen)
        {
            var (x, y, width, height) = (RiceCookerCoverX, RiceCookerCoverY, RiceCookerCoverWidth, RiceCookerCoverHeight);
            int width_ = width / 3;

            ClearBox(x, y - width_ + RiceCookerCoverHeight, width, width_);

            if (IsCoverOpen)
            {
                DrawBox(x, y - width_ + RiceCookerCoverHeight, height + 1, width_);
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
            DrawBox(MessageBoxX, MessageBoxY, MessageBoxWidth, MessageBoxHeight);
            Console.SetCursorPosition(MessageBoxX + (MessageBoxWidth - ConsoleLength(message)) / 2, MessageBoxY + 1);
            Console.Write(message);
            string? Input = Console.ReadLine();
            ClearBox(MessageBoxX, MessageBoxY, MessageBoxWidth, MessageBoxHeight);

            return Input ?? "";
        }

        static RiceCookerInfo Cook(object RCInfo_)
        {
            RiceCookerInfo RCInfo = (RiceCookerInfo)RCInfo_;
            int Rice = RCInfo.Rice - (RCInfo.Count * RicePerPerson);

            if (Rice < 0)
            {
                DrawMessageBox("쌀을 보충해주세요.");
                return RCInfo;
            }
            if (RCInfo.Water - (RCInfo.Count * WaterPerPerson) * 3 < 0)
            {
                DrawMessageBox("물을 보충해주세요.");
                return RCInfo;
            }

            DrawWater(RCInfo.Water);

            RCInfo.Rice = Rice;
            RCInfo.IsCoverOpen = true;
            RCInfo.isEmpty = false;
            DrawRice(RCInfo.Rice);
            DrawRiceCookerInfo(RCInfo);
            DrawRiceCookerBodyRicing("쌀 넣기");
            DrawRiceCookerCover(RCInfo.IsCoverOpen);
            Thread.Sleep(1000);

            for (int i = 0; i < 2; i++)
            {
                RCInfo.State = CookerProcess.Watering;
                RCInfo.Water -= RCInfo.Count * WaterPerPerson;
                DrawWater(RCInfo.Water);
                DrawRiceCookerInfo(RCInfo);
                DrawRiceCookerBodyWatering("물 넣기");
                Thread.Sleep(2000);

                RCInfo.State = CookerProcess.Washing;
                DrawRiceCookerInfo(RCInfo);
                DrawRiceCookerBodyWashing("쌀 씻기");
                Thread.Sleep(2000);

                RCInfo.State = CookerProcess.Flushing;
                DrawRiceCookerInfo(RCInfo);
                DrawRiceCookerBodyFlushing("배수");
                Thread.Sleep(1000);
            }

            RCInfo.State = CookerProcess.Cooking;
            RCInfo.Water -= RCInfo.Count * WaterPerPerson;
            RCInfo.IsCoverOpen = false;
            DrawWater(RCInfo.Water);
            DrawRiceCookerInfo(RCInfo);
            DrawRiceCookerBodyCooking("취사 중");
            DrawRiceCookerCover(RCInfo.IsCoverOpen);
            Thread.Sleep(2000);

            RCInfo.State = CookerProcess.Done;
            DrawRiceCookerInfo(RCInfo);
            DrawRiceCookerBodyCooking("취사 완료");
            Thread.Sleep(500);

            RCInfo.State = CookerProcess.Warming;
            DrawRiceCookerInfo(RCInfo);
            DrawRiceCookerBodyWarming("보온");

            return RCInfo;
        }
        static void DrawRiceCookerBodyRicing(string title)
        {
            DrawRiceCookerBody(title);
            for (int i = 0; i < RiceCookerBodyHeight - 3; i++)
            {
                Console.SetCursorPosition(RiceCookerBodyX + 1, RiceCookerBodyY + 2 + i);
                Console.Write(new string('⊙', RiceCookerBodyWidth - 2));
            }
        }
        static void DrawRiceCookerBodyRicing(string title, ConsoleColor Color)
        {
            DrawRiceCookerBody(title);
            Console.BackgroundColor = Color;

            for (int i = 0; i < RiceCookerBodyHeight - 3; i++)
            {
                Console.SetCursorPosition(RiceCookerBodyX + 1, RiceCookerBodyY + 2 + i);
                Console.Write(new string('⊙', RiceCookerBodyWidth - 2));
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void DrawRiceCookerBodyWatering(string title)
        {
            DrawRiceCookerBodyRicing(title, ConsoleColor.Blue);
        }
        static void DrawRiceCookerBodyWashing(string title)
        {
            DrawRiceCookerBody(title);
            Console.BackgroundColor = ConsoleColor.Blue;

            for (int i = 0; i < RiceCookerBodyHeight - 3; i++)
            {
                Console.SetCursorPosition(RiceCookerBodyX + 1, RiceCookerBodyY + 2 + i);
                Console.Write(new string(i % 2 == 0 ? '~' : '⊙', RiceCookerBodyWidth - 2));
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void DrawRiceCookerBodyFlushing(string title)
        {
            DrawRiceCookerBodyWatering(title);
            for (int i = 0; i < RiceCookerBodyHeight - 3; i++)
            {
                Console.SetCursorPosition(RiceCookerBodyX + 1, RiceCookerBodyY + 2 + i);
                Console.Write(new string('⊙', RiceCookerBodyWidth - 2));
                Thread.Sleep(200);
            }
        }
        static void DrawRiceCookerBodyCooking(string title)
        {
            DrawRiceCookerBodyRicing(title, ConsoleColor.Red);
        }
        static void DrawRiceCookerBodyWarming(string title)
        {
            DrawRiceCookerBody(title);
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < RiceCookerBodyHeight - 3; i++)
            {
                Console.SetCursorPosition(RiceCookerBodyX + 1, RiceCookerBodyY + 2 + i);
                Console.Write(new string('⊙', RiceCookerBodyWidth - 2));
            }

            Console.ForegroundColor = ConsoleColor.White;
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