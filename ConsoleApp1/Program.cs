namespace ConsoleApp1
{
    internal class Program
    {
        class A { }
        class B { }
        static void Main(string[] args)
        {
            int Val = 10;
            Console.WriteLine($"{Val is float} {Val is object}");

            object obj = Val;
            Console.WriteLine($"{obj is int} {obj is double}");

            Console.WriteLine($"{obj.GetType()} {typeof(object)}");

            int? Val2 = obj as int?;
            A a = new A();
            object obj2 = a;
            B b = obj2 as B;

            Console.WriteLine($"{Val2} {b == null}");

            object obj3 = null;
            Console.WriteLine($"{Convert.ToBoolean(3)} {Convert.ToBoolean(null)} {Convert.ToBoolean(obj)} {Convert.ToBoolean(obj3)}");
            Console.WriteLine(Convert.ToBoolean(obj2));
        }
    }
}