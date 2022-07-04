using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            foreach (var arg in args)
            {
                Console.Write(arg);
            }
            Console.WriteLine(true);
            Console.WriteLine("{0} {1}", 12, 3.14f);
            Console.WriteLine("{0} + {1} = {2}", 1, 2, 1 + 2);
            Console.WriteLine("{0:C} {1:P} {0:X} {0:G}", 123, 123.45);
        }
    }
}
