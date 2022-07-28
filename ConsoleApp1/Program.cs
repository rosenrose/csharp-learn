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

            public event CustomDelegate EventHandler;
            public void Print3(string str)
            {
                EventHandler(str);
            }
        }
        class B
        {
            public void Print(string str)
            {
                Console.WriteLine(str);
            }
            public void Print2(string str)
            {
                Console.WriteLine(str + "hello");
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
            p -= a.Print;
            p("c");
            Console.WriteLine(p.GetType());
            p -= a.Print2;
            //Console.WriteLine(p.GetType());

            B b = new();
            a.EventHandler += b.Print;
            a.EventHandler += b.Print2;
            a.Print3("hello");
            a.EventHandler -= b.Print;
            a.EventHandler -= b.Print2;
            a.Print3("hello");
        }
    }
}