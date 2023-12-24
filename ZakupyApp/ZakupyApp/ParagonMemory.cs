
using System.Diagnostics;

namespace ZakupyApp
{
    public class ParagonMemory : ParagonBase
    {
        public ParagonMemory(string shop, string data)
            : base(shop, data)
        {
        }
        private List<float> sumes = new List<float>();

        public override event ParagonAddedDelegate ParagonAdded;

        public override void AddParagon(float suma)
        {
            if (suma > 0)
            {
                this.sumes.Add(suma);

                if (ParagonAdded != null)
                {
                    ParagonAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Invalid grade value");
            }

        }

        public override void AddParagon(string suma)
        {
            if (float.TryParse(suma, out float result))
            {
                this.AddParagon(result);
            }
            else
            {
                throw new Exception("String is not a float");
            }

        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            foreach (var sume in this.sumes)
            {
                statistics.AddParagon(sume);
            }
            return statistics;

        }
    }



}

