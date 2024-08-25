using InvestWebAPI.Intefaces;
using InvestWebAPI.Services;
//using Microsoft.Extensions.DependencyInjection;
public class ShippingServiceFactory : IShippingServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    public ShippingServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IShippingService GetShippingService(string provider)
    {
        switch (provider.ToLower())
        {
            case "fedex":
                return _serviceProvider.GetService<FedexShippingService>();
            case "car":
                return _serviceProvider.GetService<CarShippingService>();
            // Thêm các case khác
            default:
                throw new ArgumentException("Unknown shipping provider");
        }
    }
}