namespace ConsoleApp1
{
    internal class Program
    {
        interface I
        {
            void Print();
        }
        interface I2
        {
            void Print();
        }
        class A : I, I2
        {
            public void Print() => Console.WriteLine("A print");
        }
        static void Main(string[] args)
        {
            A a = new();
            a.Print();
        }
    }
}