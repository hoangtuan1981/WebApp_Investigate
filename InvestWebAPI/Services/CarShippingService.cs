using InvestWebAPI.Intefaces;

namespace InvestWebAPI.Services
{
    public class CarShippingService : IShippingService
    {
        public string ShippItem()
        {
           Console.WriteLine("Shipping by Car");
           return "Shipped item by Car";
        }
    }
}
