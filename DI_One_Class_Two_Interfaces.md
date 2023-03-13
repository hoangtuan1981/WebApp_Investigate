Class OneClassTwoInterfacesService : ICalculate, IHelper
    implement "Calculate", "GetName" method.

Startup.cs: 
    services.AddTransient<IReader, OneClassTwoInterfacesService>();
    services.AddTransient<IHelper, OneClassTwoInterfacesService>();

HomeController.cs
    public HomeController( ICalculate calculate, IEnumerable<ICalculate> calculates,
    //case: 1 class - 2 interfaces
    IReader reader, IHelper helper) 
    --> Results: 
    OneClassTwoInterfacesService will be resolved for 2 interfaces IReader, IHelper.
    Each interface referrence to 1 instance of class:
    IReader --> 1 instance of OneClassTwoInterfacesService (Instance A).
    IHelper --> 1 instance of OneClassTwoInterfacesService (Instance B).

