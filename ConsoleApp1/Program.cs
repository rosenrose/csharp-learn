namespace ConsoleApp1
{
    internal class Program
    {
        class A
        {
            private int num;
            public int Num
            {
                get => num; set => num = value;
            }
            public void print()
            {
                num = 10;
                Num = 20;
                Console.WriteLine($"{Num} {Num.GetType()}");
            }
            private int[] number = { 1, 2, 3 };
            public int this[int index]
            {
                get
                {
                    if (index < -1 * number.Length || index >= number.Length)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    return index < 0 ? number[index + number.Length] : number[index];
                }
                set
                {
                    if (index < -1 * number.Length || index >= number.Length)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    if (index < 0)
                    {
                        index += number.Length;
                    }

                    number[index] = value;
                }
            }
        }

        static void Main(string[] args)
        {
            int[,] a = { { 1, 2, }, { 3, 4 } };
            int[][] b = { new int[] { 1, 2, 3 }, new int[] { 4 } };

            A[] c = { new(), new(), new() };
            c[0].print();

            c[1][-1] = 10;
            Console.WriteLine($"{c[1][-2]} {c[1][-1]}");

            //c[1][-4] = 99;
            Console.WriteLine(c[1][3]);
        }
    }
}