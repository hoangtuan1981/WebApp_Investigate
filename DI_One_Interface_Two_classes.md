Classes CalculateLineService, CalculateTotalAmountService : ICalculate
    implement "Calculate" method.

Startup.cs: 
    services.AddTransient<ICalculate, CalculateLineService>();
    services.AddTransient<ICalculate, CalculateTotalAmountService>();

HomeController.cs
    public HomeController( ICalculate calculate, IEnumerable<ICalculate> calculates)
    --> Results:
    calculate object:  will resolved with latest class: CalculateTotalAmountService.
    calculates object: will resolved all classes: CalculateLineService, CalculateTotalAmountService.
