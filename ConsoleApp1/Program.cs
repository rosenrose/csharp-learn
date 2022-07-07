using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 3.14f;
            float b = 10.0f;
            Console.WriteLine("{0} {1:f0} {2:f1}", a, b, a + b);

            int? c = null;
            string d = null;
            Console.WriteLine("{0} {1} {2} {3}", c, c.HasValue, c == null, d);

            var Var1 = a.ToString();
            var Var2 = double.Parse(Var1);
            var Var3 = Convert.ToSingle(Var2);
            Console.WriteLine("{0} {1}", Var2, Var3);

            int m = 123;
            object obj = m;
            m = 30;
            int n = (int)obj;
            obj = "zz";
            obj = true;
            obj = 3.14;
            Console.WriteLine("{0} {1} {2}", m, n, obj);

            int i = 10;
            int k = i;
            i = 20;
            Console.WriteLine(k);
        }
    }
}
