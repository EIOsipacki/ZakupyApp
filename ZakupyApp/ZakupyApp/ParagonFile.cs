
using System.Diagnostics;

namespace ZakupyApp
{
    internal class ParagonFile : ParagonBase
    {
        private const string fileName = "paragony.txt";

        public ParagonFile(int year) : base(year)
        {
        }

        public override event ParagonAddedDelegate ParagonAddedEvent;

        public override void AddParagon(decimal suma)
        {
            if (suma >= 0 && suma <= 100)
            {
                if (ParagonAddedEvent != null)
                {
                    ParagonAddedEvent(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Invalid grade value");
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            string line;
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    int kollines = 1;
                    line = reader.ReadLine();
                    
                    while (line != null)
                    {
                        string[] elements = line.Split(';');
                        if (elements.Length == 3)
                        {
                            decimal rezult = decimal.Parse(elements[2].Trim());
                            statistics.AddParagon(rezult);
                        }
                        else
                        {
                            Console.WriteLine($"Błąd w linii: {line}. Pominięto.");
                        }
                        line = reader.ReadLine();
                        kollines++;
                    }
                }
            }
            return statistics;
        }

        public void WriteLinieFromFileEqualNumaber (decimal number)
        {
            string line;
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    int kollines = 1;
                    line = reader.ReadLine();

                    while (line != null)
                    {
                        string[] elements = line.Split(';');
                        string nazwaSklepu = elements[0].Trim();
                        string dataZakupu = elements[1].Trim();
                        decimal sumaZakupu = decimal.Parse(elements[2].Trim());
                        if ((elements.Length == 3) && (sumaZakupu == number))
                        {
                            Console.WriteLine(line);
                        }
                        line = reader.ReadLine();
                        kollines++;
                    }
                }
            }
        }
    }
}
