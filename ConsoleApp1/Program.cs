namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new("test.txt", FileMode.Create);
            using (StreamWriter sw = new(fs))
            {
                sw.Write(12);
            }
            using (StreamWriter sw2 = new("test.txt", true))    //append: true
            {
                sw2.WriteLine("dd");
                sw2.WriteLine(3.14);
                sw2.WriteLine(new int[] { 1, 2 });
            }

            string? pPath = Environment.ProcessPath;
            string cPath = Environment.CurrentDirectory;
            File.WriteAllText("test2.txt", "write");
            File.WriteAllLines("test2.txt", new string[] { pPath!, cPath });

            //Console.ReadLine();

            FileStream fs2 = new("test.txt", FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new(fs2))
            {
                Console.WriteLine(sr.Read());
                Console.WriteLine(sr.ReadLine());
            }
            string Line;
            using (StreamReader sr2 = new("test.txt"))
            {
                Line = sr2.ReadLine()!;
                Console.WriteLine($"1 {Line} {Line[^1] == '\n'}");
                Console.WriteLine($"2 {sr2.ReadToEnd()}");
            }

            Console.WriteLine($"3 {File.ReadAllText("test2.txt")}");
            Console.Write("4 ");
            foreach (var line in File.ReadAllLines("test2.txt"))
            {
                Console.WriteLine($"{line} {line[^1] == '\n'}");
            }
            Console.Write("5 ");
            foreach (var line in File.ReadLines("test2.txt"))
            {
                Console.WriteLine($"{line} {line[^1] == '\n'}");
            }
        }
    }
}