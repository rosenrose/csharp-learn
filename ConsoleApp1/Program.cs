namespace ConsoleApp1
{
    internal class Program
    {
        class A
        {
            private void PrintPrivate() => Console.WriteLine("A private");
            protected void PrintProtected() => Console.WriteLine("A protected");
            public virtual void PrintPublic() => Console.WriteLine("A public");
            protected int Num;
            public A(int num)
            {
                Num = num;
                Console.WriteLine("A Constructor");
            }
            ~A() => Console.WriteLine("A finalizer");
        }
        class B : A
        {
            public override void PrintPublic() => Console.WriteLine("B public");
            public void Print()
            {
                //PrintPrivate();
                PrintProtected();
                PrintPublic();
            }
            private int Num;
            public B(int num) : base(num)
            {
                Num = num - 5;
                Console.WriteLine($"B Constructor {base.Num} {Num}");
            }
            ~B() => Console.WriteLine("B finalizer");
        }
        static void Main(string[] args)
        {
            B b = new(10);
            b.Print();

            A a = b;
            //a.Print();
            a.PrintPublic();
        }
    }
}