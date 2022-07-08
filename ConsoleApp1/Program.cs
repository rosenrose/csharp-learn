using System;

namespace ConsoleApp1
{
    class Program
    {
        public struct Struct
        {
            public const int a = 0;
            public static int b = 1;
            public int Val;

            public Struct(int Val_)
            {
                Val = Val_;
            }
        }

        class Class
        {
            public int Val;
        }

        public struct Score
        {
            public int Kor, Eng, Math, Total;
            public float Avg;

            public void Compute()
            {
                Total = Kor + Eng + Math;
                Avg = Total / 3.0f;
            }
        }

        enum Days : byte { Sun = 0, Mon, Tue, Wed, Thu, Fri, Sat }

        static void Main(string[] args)
        {
            Struct s;
            s.Val = 5;
            Console.WriteLine($"{Struct.a} {Struct.b} {s} {s.Val}");

            Struct s2 = new Struct();
            Struct s3 = s2;
            s3.Val = 20;
            Console.WriteLine($"{s2.Val} {s3.Val}");

            Class c = new Class();
            Console.WriteLine($"{c} {c.Val}");
            Class c2 = c;
            c.Val = 15;
            Console.WriteLine($"{c.Val} {c2.Val}");

            Score score = new Score();
            score.Kor = 80;
            score.Eng = 77;
            score.Math = 19;
            score.Compute();
            Console.WriteLine($"총점: {score.Total}, 평균: {score.Avg:f1}");

            Days Wed = Days.Wed;
            Console.WriteLine($"{Wed} {(int)Wed}");
        }
    }
}
