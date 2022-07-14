namespace ConsoleApp1
{
    internal class Program
    {
        struct Grade
        {
            public int Kor, Eng, Math, Total;
            public double Avg;
        }

        static void Main(string[] args)
        {
            Console.Write("number of students: ");
            int Count = int.Parse(Console.ReadLine()!);

            File.WriteAllText("test.txt", $"number of students: {Count}\n");
            Grade[] Grades = new Grade[Count];

            for (int i = 0; i < Count; i++)
            {
                Console.Write($"student {i + 1} grades: ");
                (Grades[i].Kor, Grades[i].Eng, Grades[i].Math) = Console.ReadLine()!.Split(new char[] { ' ' }) switch
                {
                    var list => (int.Parse(list[0]), int.Parse(list[1]), int.Parse(list[2]))
                };
                Grades[i].Total = Grades[i].Kor + Grades[i].Eng + Grades[i].Math;
                Grades[i].Avg = Math.Round(Grades[i].Total / 3.0f);
            }
            File.AppendAllLines("test.txt", Grades.Select(grade => $"{grade.Kor} {grade.Eng} {grade.Math} {grade.Total} {grade.Avg:f1}").ToArray());

            Console.Write("file name: ");
            string filename = Console.ReadLine()!;
            using (StreamReader sr = new(filename))
            {
                Count = int.Parse(sr.ReadLine()!.Split(new char[] { ':' })[1]);
                Console.WriteLine($"count: {Count}");

                for (int i = 0; i < Count; i++)
                {
                    string[] grades = sr.ReadLine()!.Split(new char[] { ' ' });
                    Console.WriteLine($"kor: {grades[0]}, eng: {grades[1]}, math: {grades[2]}, total: {grades[3]}, average: {grades[4]}");
                }
            }
        }
    }
}