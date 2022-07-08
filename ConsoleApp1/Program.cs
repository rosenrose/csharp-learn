using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 2;
            int b = new int();
            Console.WriteLine($"{a} {b}");

            int[] array = { 1, 2, 3 };
            int[] array2 = array;
            array2[1] = 9;
            Console.WriteLine($"{array[0]} {array[1]} {array[2]}");
        }
    }
}
