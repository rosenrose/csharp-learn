using System.Runtime.Serialization.Formatters.Binary;
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
            Data[] DataArr = new Data[2] { new(10, 3.14, "hello"), new(5, 0.5, "가나ㄱㅋ") };
            BinaryFormatter bf = new();

            using (FileStream fs = File.Open("test.dat", FileMode.Create))
            {
                bf.Serialize(fs, DataArr);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.HangulSyllables, UnicodeRanges.HangulCompatibilityJamo)
            };
            File.WriteAllText("test.txt", JsonSerializer.Serialize(DataArr, options));


            using (FileStream fs = File.Open("test.dat", FileMode.Open))
            {
                DataArr = (Data[])bf.Deserialize(fs);
            }
            foreach (var data in DataArr)
            {
                Console.WriteLine($"{data.Var1} {data.Var2} {data.Var3} {data.Var4}");
            }

            DataArr = JsonSerializer.Deserialize<Data[]>(File.ReadAllText("test.txt"))!;
            foreach (var data in DataArr)
            {
                Console.WriteLine($"{data.Var1} {data.Var2} {data.Var3} {data.Var4}");
            }
        }
    }
}