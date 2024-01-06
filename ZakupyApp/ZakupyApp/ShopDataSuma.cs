

namespace ZakupyApp
{
    internal class ShopDataSuma
    {
     public ShopDataSuma(string shop, DateTime date, decimal suma)
        {
            this.Shop = shop;
            this.Date = date;
            this.Suma = suma;
        }
        public  string Shop { get;  set;  }
        public DateTime Date { get; set; }
        public decimal Suma { get; set;  }
    }
}
