namespace ConsoleApp1
{
    internal class Program
    {
        delegate void CustomDelegate(string s);
        class A
        {
            public void Print(string str)
            {
                Console.WriteLine(str);
            }
            public void Print2(string _)
            {
                Console.WriteLine("ㅋㅋ");
            }
        }
        static void Main(string[] args)
        {
            var del = delegate (int x) { return x * x; };
            Func<int, int> lambda = x => x * x;

            Console.WriteLine($"{lambda(2)} {del(4)}");

            A a = new();
            CustomDelegate p = a.Print;
            p("a");
            p += a.Print2;
            p("b");
        }
    }
}