using static ZakupyApp.ParagonBase;

namespace ZakupyApp
{
     internal interface IParagon
    {
     
            int Year { get;  set;  }       
            void AddParagon(decimal suma);
            Statistics GetStatistics();
                       
            event ParagonAddedDelegate ParagonAddedEvent;
        
    }
}
