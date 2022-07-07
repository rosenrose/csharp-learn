using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0} {1}", Console.Read(), Console.Read());

            ConsoleKeyInfo KeyInfo;
            do
            {
                KeyInfo = Console.ReadKey(true);
                Console.WriteLine("{0} {1} {2}", KeyInfo.Key, KeyInfo.KeyChar, KeyInfo.Modifiers);
            } while (KeyInfo.Key != ConsoleKey.Escape);
            Console.WriteLine(Console.ReadLine());

            int Kor, Eng, Math, Total;
            float Avg;

            Console.Write("국어: ");
            Kor = int.Parse(Console.ReadLine());
            Console.Write("영어: ");
            Eng = int.Parse(Console.ReadLine());
            Console.Write("수학: ");
            Math = int.Parse(Console.ReadLine());
            Total = Kor + Eng + Math;
            Avg = Total / 3.0f;
            Console.WriteLine("{0} {1} {2} {3} {4:f1}", Kor, Eng, Math, Total, Avg);
        }
    }
}
