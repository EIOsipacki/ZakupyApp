
namespace ZakupyApp
{
    public class ParagonMemory : ParagonBase
    {
        public ParagonMemory(int year)
            : base(year)
        {
        }

        private List<decimal> sumes = new List<decimal>();

        public int Year { get; set; }

        public override event ParagonAddedDelegate ParagonAddedEvent;
        public override void AddParagon(decimal suma)
        {
            if (suma > 0)
            {
                this.sumes.Add(suma);

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
            Statistics statistics = new Statistics();
            foreach (var sume in this.sumes)
            {
                statistics.AddParagon(sume);
            }
            return statistics;
        }
    }
}

