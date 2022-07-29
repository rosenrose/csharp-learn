namespace ConsoleApp1
{
    internal class Program
    {
        class A
        {
            public virtual void Print() => Console.WriteLine("A print");
        }
        class B : A
        {
            public override void Print() => Console.WriteLine("B print");
            public int num = 3;
        }
        static void Main(string[] args)
        {
            A a = new();
            a.Print();

            A a2 = new B();
            a2.Print();
            // Console.WriteLine(a2.num);
        }
    }
}