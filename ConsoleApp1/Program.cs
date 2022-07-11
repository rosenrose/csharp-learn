namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] Days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            MutateArray(Days);
            PrintArray(Days);

            string[] KorDays = NewArray(Days);
            PrintArray(KorDays);

            PrintArray(CreateArray(6));
            PrintArray(CreateArray2(2, 4));

            Array.Clear(Days);
            Console.WriteLine($"{Days[0] is null}");

            string[] ClonedDays = (string[])KorDays.Clone();
            KorDays[0] = "AA";
            Console.WriteLine(ClonedDays[0]);
        }

        static void MutateArray(string[] arr)
        {
            string[] KorDays = { "일", "월", "화", "수", "목", "금", "토" };
            foreach (var (Day, i) in KorDays.Select((v, i) => (v, i)))
            {
                arr[i] = Day;
            }
        }

        static string[] NewArray(string[] arr)
        {
            string[] KorDays = { "일", "월", "화", "수", "목", "금", "토" };
            return arr.Select((_, i) => KorDays[i]).ToArray();
        }

        static int[] CreateArray(int size)
        {
            return new int[size].Select((_, i) => i + 1).ToArray();
        }

        static int[,] CreateArray2(int row, int col)
        {
            int[,] Arr = new int[row, col];
            for (int i = 0; i < Arr.Length; i++)
            {
                Arr[i / col, i % col] = (i + 1) * 2;
            }
            return Arr;
        }

        static void PrintArray<T>(T[] Arr)
        {
            foreach (var item in Arr)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
        static void PrintArray<T>(T[,] Arr)
        {
            foreach (var item in Arr)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}