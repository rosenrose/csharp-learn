using System.Collections;

namespace ConsoleApp1
{
    internal class Program
    {
        class A
        {
            private int[] number = new int[5];
            public int this[int index]
            {
                get => number[index];
                set => number[index] = value;
            }
        }
        class B
        {
            private string str;
            public string this[string index]
            {
                get => $"{str} + {index}";
                set => str = index.ToUpper() + value;
            }
        }
        class C
        {
            ArrayList arrayList = new();
            public object? this[int index]
            {
                get => index < 0 || index >= arrayList.Count ? null : arrayList[index];
                set
                {
                    if (index < 0 || index > arrayList.Count)
                    {
                        return;
                    }

                    if (index == arrayList.Count)
                    {
                        arrayList.Add(value);
                        return;
                    }

                    arrayList[index] = value;
                }
            }
        }
        static void Main(string[] args)
        {
            A a = new();
            a[1] = 3;
            Console.WriteLine($"{a[0]} {a[1]}");

            B b = new();
            b["abc"] = "def";
            Console.WriteLine(b["foo"]);

            C c = new();
            c[0] = 12;
            c[1] = "foo";
            c[2] = true;

            c[1] = "bar";
            Console.WriteLine($"{c[0]} {c[1]} {c[2]}");
        }
    }
}