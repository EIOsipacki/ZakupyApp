using static ZakupyApp.ParagonBase;

namespace ZakupyApp
{
    public abstract class ParagonBase : IParagon
    {
        public ParagonBase(string shop, string data)
        {
            this.Shop = shop;
            this.Data = data;
        }

        public  string Shop { get; private set;  }
        public  string Data { get; private set; }

        public delegate void ParagonAddedDelegate(object sender, EventArgs args);

        public abstract event ParagonAddedDelegate ParagonAdded;

        public abstract void AddParagon(float suma);
        public abstract void AddParagon(string suma);
        public abstract Statistics GetStatistics();
    }
}
