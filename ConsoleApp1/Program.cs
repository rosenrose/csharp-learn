namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (int, double) t1 = (10, 3.0);
            Console.WriteLine($"{t1.Item1} {t1.Item2} {t1}");
            t1.Item1 = 5;
            Console.WriteLine(t1);

            var t2 = (10, true, 9.0f, "aa");
            Console.WriteLine(t2);

            (int a, bool b) t3 = (1, false);
            Console.WriteLine($"{t3.a} {t3.b}");

            var v = new { Amount = 108, Message = "Hello" };
            Console.WriteLine(v);

            var t4 = ("home", 3.6);
            var (destination, distance) = t4;
            Console.WriteLine($"{destination} to {distance}km");

            var array = new[] { 1, 0, 3, 4 };
            Range p = 1..3;
            Console.WriteLine($"{array[^1]} {array[p].Length}");

            string[] words = { "bot", "apple", "apricot" };
            int minimalLength = words.Where(w => w.StartsWith("a")).Min(w => w.Length);
            Console.WriteLine(minimalLength);
        }
    }
}