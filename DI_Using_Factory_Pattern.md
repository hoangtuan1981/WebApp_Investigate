**Question**
1. Question 1:
  có 1 api endpoint export shipping qua các hệ thống như fedex, car ....
  làm sao đề thiết kế .net api dependency inversion cho trường hợp này?
  --> Answer: dùng DI + factory pattern.
2. Question 2: với answer cho question 1. implement 1 api chỉ shipping bằng car
  --> answer: add và dùng DI cho car.

**Practise**
1. practise cho Question 1:
  DIWithFactoryPatternController.cs 
  Declare interface: 
    1. IShippingService.cs, CarShippingService.cs, and FedexShippingService.cs
    2. Configuration CarShippingService and FedexShippingService in program.cs: It's important
      builder.Services.AddTransient<FedexShippingService>();
      builder.Services.AddTransient<CarShippingService>();
2. practise cho Question 2:
  DI for controller constructor:
  builder.Services.AddTransient<IShippingService, CarShippingService>();
  

**project:**
InvestWebAPI

http://localhost:5067/swagger/index.html

**test data:**
{
  "userInfor": "tesytyt",
  "item": "t-shirt",
  "method": "car"
}