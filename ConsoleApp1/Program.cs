using System.Collections;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int Val = 15;

            switch (Val)
            {
                case < 10:
                    Console.WriteLine(Val + 10);
                    break;
                case > 10:
                    Console.WriteLine(Val - 10);
                    break;
            }

            ArrayList List = new();
            List.Add(1);
            List.Add("a");
            List.Add(true);

            foreach (var i in List)
            {
                Console.WriteLine($"{i} {i.GetType()}");
            }

            try
            {
                int i = new[] { 1, 2 }[-2];
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} {e.StackTrace}");
                Console.WriteLine(new[] { 1, 2 }[^2]);
            }

            throw new Exception("zz");
        }
    }
}