using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DIWithFactoryPatternController : ControllerBase
{
    private readonly IShippingServiceFactory _shippingServiceFactory;
    public DIWithFactoryPatternController(IShippingServiceFactory shippingServiceFactory)
    {
        _shippingServiceFactory = shippingServiceFactory;
    }

    [HttpGet("PopulateShippingItem")]
    //public ActionResult PopulateShippingItem([FromBody] ShippingInfor shippingInfor)
    public ActionResult PopulateShippingItem([FromQuery] string userInfor, [FromQuery] string item, [FromQuery] string method)
    {
        var shippingService = _shippingServiceFactory.GetShippingService(method);

        return Ok(shippingService.ShippItem());
    }

}
public record ShippingInfor(string userInfor, string item, string method)
{

}

