namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 999999);
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