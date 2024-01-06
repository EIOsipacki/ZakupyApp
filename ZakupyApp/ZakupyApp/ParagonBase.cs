namespace ZakupyApp
{
    public abstract class ParagonBase : IParagon
    {
        public ParagonBase(int year)
        {
            this.Year = year;
        }

        public  int Year { get; set; }

        public delegate void ParagonAddedDelegate(object sender, EventArgs args);

        public abstract event ParagonAddedDelegate ParagonAddedEvent;

        public abstract void AddParagon(decimal suma);
        public abstract Statistics GetStatistics();
       
    }
}
