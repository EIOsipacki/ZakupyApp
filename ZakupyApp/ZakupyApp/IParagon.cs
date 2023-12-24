using static ZakupyApp.ParagonBase;

namespace ZakupyApp
{
     internal interface IParagon
    {
        
            string Shop { get; }
            string Data {  get;}   
                        
            void AddParagon(float suma);
            void AddParagon(string suma);
            Statistics GetStatistics();

            event ParagonAddedDelegate ParagonAdded;
        
    }
}
