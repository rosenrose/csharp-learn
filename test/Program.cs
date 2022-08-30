namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
        }

        static void ConvertTest()
        {
            object? a = null;
            int? b;

            b = (int?)a;
            Console.WriteLine(b.ToString());
            b = 10;
            Console.WriteLine(b.ToString());

            GenderEnum c = (GenderEnum)1;
            Console.WriteLine(c.ToString());

            ulong d = 0;
            c = (GenderEnum)d;
            Console.WriteLine(c.ToString());
        }
        enum GenderEnum { Male = 0, Female = 1 };

        static void DictFromEnumerable()
        {
            string[] Lines = File.ReadAllLines("test.txt");
            Dictionary<string, int> Dict = new(Lines.Select(line => line.Split(' ') switch
            {
                var list => new KeyValuePair<string, int>(list[0], int.Parse(list[1]))
            }));

            foreach (KeyValuePair<string, int> kvp in Dict)
            {
                Console.WriteLine(kvp);
            }

        }
        static void CharWidth()
        {
            File.WriteAllText("log.txt", "");

            for (char c = char.MinValue; c <= char.MaxValue; c++)
            {
                try
                {
                    Console.Write(c);
                    File.AppendAllText("log.txt", $"{c} {(int)c} {Console.GetCursorPosition().Left}\n");
                }
                catch
                {
                    File.AppendAllText("log.txt", $"{(int)c} -1\n");
                }

                Console.SetCursorPosition(0, 0);
            }
        }
    }
}