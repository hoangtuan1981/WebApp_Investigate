using Microsoft.AspNetCore.Mvc;
using InvestWebAPI.Intefaces;

[ApiController]
[Route("api/[controller]")]
public class DIWithFactoryPatternController : ControllerBase
{
    private readonly IShippingServiceFactory _shippingServiceFactory;
    private readonly IShippingService _carShippingService;
    public DIWithFactoryPatternController(IShippingServiceFactory shippingServiceFactory, IShippingService carShippingService)
    {
        _shippingServiceFactory = shippingServiceFactory;
         _carShippingService = carShippingService;
    }

    [HttpGet("PopulateShippingItem")]
    //public ActionResult PopulateShippingItem([FromBody] ShippingInfor shippingInfor)
    public ActionResult PopulateShippingItem([FromQuery] string userInfor, [FromQuery] string item, [FromQuery] string method)
    {
        var shippingService = _shippingServiceFactory.GetShippingService(method);

        return Ok(shippingService.ShippItem());
    }

    [HttpGet("PopulateCarItem")]
    //public ActionResult PopulateShippingItem([FromBody] ShippingInfor shippingInfor)
    public ActionResult PopulateCarItem([FromQuery] string userInfor, [FromQuery] string item)
    {
        var shippingService = _carShippingService.ShippItem();

        return Ok(shippingService);
    }
}
public record ShippingInfor(string userInfor, string item, string method)
{

}

