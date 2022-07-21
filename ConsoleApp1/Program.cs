using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace ConsoleApp1
{
    internal class Program
    {
        [Serializable]
        struct Data
        {
            public int Var1 { get; set; }
            public double Var2 { get; set; }
            public string Var3 { get; set; }
            [NonSerialized] public bool Var4;
            public Data(int Var1_, double Var2_, string Var3_) => (Var1, Var2, Var3, Var4) = (Var1_, Var2_, Var3_, true);
        }

        static void Main(string[] args)
        {
            List<Data> list = new()
            {
                new(3, 5.14, "world"),
                new(10, 9.0, "ㅎㄴ거카"),
            };

            var options = new JsonSerializerOptions
            {
                //WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            File.WriteAllText("test.txt", JsonSerializer.Serialize(list, options));

            list = JsonSerializer.Deserialize<List<Data>>(File.ReadAllText("test.txt"))!;
            foreach (var data in list)
            {
                Console.WriteLine($"{data.Var1} {data.Var2} {data.Var3} {data.Var4}");
            }
        }
    }
}