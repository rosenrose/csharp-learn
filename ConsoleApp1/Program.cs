using System;

namespace ConsoleApp1
{
    class Program
    {
        static bool BoolVar;
        static void Main(string[] args)
        {
            bool LocalBoolVar = true;
            Console.WriteLine("{0} {1}", BoolVar, LocalBoolVar);

            int Number = '7';
            Console.WriteLine("{0} '{1}'", Number, (char)(Number + 1));
            Number = '가';
            Console.WriteLine("{0} '{1}'", Number, (char)(Number + 1));
            float Number2 = '가';
            Console.WriteLine("{0} '{1}'", Number2, (char)(Number2 + 2.3));

            byte x = 1, y = 2;
            int Result = x + y;
            Console.WriteLine(Result);

            Console.WriteLine("{0} ~ {1}", byte.MinValue, byte.MaxValue);
            Console.WriteLine("{0} ~ {1}", sbyte.MinValue, sbyte.MaxValue);
            Console.WriteLine("{0} ~ {1}", short.MinValue, short.MaxValue);
            Console.WriteLine("{0} ~ {1}", int.MinValue, int.MaxValue);
            Console.WriteLine("{0} ~ {1}", long.MinValue, long.MaxValue);
            Console.WriteLine("{0} ~ {1} {2} {3} {4}", float.MinValue, float.MaxValue, float.NegativeInfinity, float.PositiveInfinity, float.NaN);
            Console.WriteLine("{0} ~ {1}", double.MinValue, double.MaxValue);
            Console.WriteLine("{0} ~ {1}", decimal.MinValue, decimal.MaxValue);

            string str = "ab하하";
            str += "C#";
            Console.WriteLine("{0} {1} {2} {3}", str, str[4], str.Length, str == "");
            string path = @"C:\temp\test.txt";
            string path2 = "C:\\temp\\test.txt";
            Console.WriteLine("{0} {1}", path, path2);
        }
    }
}
