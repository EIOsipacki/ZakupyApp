
namespace ZakupyApp
{
    public class Statistics
    {   

        public decimal Min { get; private set; }

        public decimal Max { get; private set; }

        public decimal Average
        {
            get
            {
                return this.Sum / this.Count;
            }
        }
        public decimal Sum { get; private set; }

        public int Count { get; private set; }


        public Statistics()
        {
            this.Count = 0;
            this.Sum = 0;
            this.Max = decimal.MinValue;
            this.Min = decimal.MaxValue;
        }
        public void AddParagon(decimal suma)
        {
            this.Count++;
            this.Sum += suma;
            this.Min = Math.Min(suma, this.Min);
            this.Max = Math.Max(suma, this.Max);
        }
        public void WriteLineStatistics()
        {
            Console.WriteLine("--------------- Ztatystyka zakupow według wprowadzonych paragonow ---------------");
            Console.WriteLine($"Average zakupów: {Average:N2}");
            Console.WriteLine($"Min zakup : {Min:N2}");
            Console.WriteLine($"Max zakup : {Max:N2}");
            Console.WriteLine();
        }
    }
}
