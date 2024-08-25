using InvestWebAPI.Intefaces;
public interface IShippingServiceFactory
{
    IShippingService GetShippingService(string provider);
}