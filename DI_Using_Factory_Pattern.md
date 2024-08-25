**Question**
có 1 api endpoint export shipping qua các hệ thống như fedex, car ....
làm sao đề thiết kế .net api dependency inversion cho trường hợp này?

**Practise**
DIWithFactoryPatternController.cs 
Declare interface: 
1. IShippingService.cs, CarShippingService.cs, and FedexShippingService.cs
2. Configuration CarShippingService and FedexShippingService in program.cs: It's important
builder.Services.AddTransient<FedexShippingService>();
builder.Services.AddTransient<CarShippingService>();

**project:**
InvestWebAPI

http://localhost:5067/swagger/index.html

**test data:**
{
  "userInfor": "tesytyt",
  "item": "t-shirt",
  "method": "car"
}