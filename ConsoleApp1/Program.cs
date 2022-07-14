namespace ConsoleApp1
{
    internal class Program
    {
        struct Data
        {
            public int Var1;
            public double Var2;
            public Data(int Var1_, double Var2_) => (Var1, Var2) = (Var1_, Var2_);
        }

        static void Main(string[] args)
        {
            using (BinaryWriter bw = new(File.Open("test.dat", FileMode.Create)))
            {
                bw.Write(12);
                bw.Write(3.14);
                bw.Write("hello");
                bw.Write("가나");
            }

            FileStream fs = File.Open("test.dat", FileMode.Open);
            using (BinaryReader br = new(fs))
            {
                Console.WriteLine($"{br.ReadInt32()} {br.ReadDouble()} {br.ReadString()} {br.ReadString()}");
                fs.Seek(0, SeekOrigin.Begin);
                Console.WriteLine($"{br.ReadInt32()} {br.ReadInt32()} {br.ReadInt32()} {br.ReadInt64()} {br.ReadInt16()} {br.ReadInt16()}");
                fs.Seek(0, SeekOrigin.Begin);
                Console.WriteLine($"{br.ReadDouble()} {br.ReadDouble()} {br.ReadDouble()}");
                fs.Seek(0, SeekOrigin.Begin);
                Console.WriteLine($"{br.ReadString()}");
            }


            Data[] DataArr = new Data[2] { new(10, 3.14), new(5, 0.5) };

            using (BinaryWriter bw = new(File.Open("test.dat", FileMode.Create)))
            {
                foreach (var data in DataArr)
                {
                    bw.Write(data.Var1);
                    bw.Write(data.Var2);
                }
            }

            using (BinaryReader br = new(File.Open("test.dat", FileMode.Open)))
            {
                do
                {
                    try
                    {
                        Console.WriteLine($"{br.ReadInt32()} {br.ReadDouble()}");
                    }
                    catch (EndOfStreamException e)
                    {
                        break;
                    }
                } while (true);
            }
        }
    }
}