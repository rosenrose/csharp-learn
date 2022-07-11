namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2 };
            Console.WriteLine(arr.Count());

            int[,] arr2 = new int[3, 2];
            Console.WriteLine($"{arr2[0, 0]} {arr2.Length} {arr2.Rank} {arr2.GetLength(0)} {arr2.GetType()}");

            int[,] arr3 = { { 1, 2 }, { 3, 4 } };
            foreach (var i in arr3)
            {
                Console.WriteLine(i);
            }

            for (int i = 0; i < arr3.Rank; i++)
            {
                for (int j = 0; j < arr3.GetLength(0); j++)
                {
                    Console.WriteLine($"val: {arr3[i, j]} {i} {j}");
                }
            }

            int[][] arr4 = { new int[] { 1, 2 }, new int[] { 4, 6, 8 } };
            Console.WriteLine($"{arr4.Length} {arr4.Rank} {arr4.GetLength(0)} {arr4.GetType()} {arr4.Count()}");
            foreach (var (row, i) in arr4.Select((v, i) => (v, i)))
            {
                foreach (var (value, j) in row.Select((v, i) => (v, i)))
                {
                    Console.WriteLine($"val: {value} {i} {j}");
                }
            }

            int[][][] arr5 = {
                new int[][] {
                    new int[] { 1, 2 },
                    new int[] { 4, 6, 8 }
                },
                new int[][] {
                    new int[] { 1 },
                    new int[] { 10, 10, 11, 7 }
                }
            };
            int[][,] arr6 = {
                new int[,] { { 1 }, { 2 } },
                new int[,] { { 8 }, { 9 } }
            };
        }
    }
}