using InvestWebAPI.Intefaces;

namespace InvestWebAPI.Services
{
    public class FedexShippingService : IShippingService
    {
        public string ShippItem()
        {
           Console.WriteLine("Shipping by Fedex");
           return "Shipped item by Fedex method";
        }
    }
}
