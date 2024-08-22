using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private int _sleepMiliseconds = 1000;
    [HttpGet("TaskDelay")]
    [Produces("application/json")]
    //[OpenApiOperation("GetEmployees", Summary = "Gets a list of all employees.")]
    public ActionResult<IEnumerable<Employee>> Get1()
    {
        Console.WriteLine("TaskDelay Endpoint: Begin");
        var employees = new List<Employee>
            {
                new Employee(1, "John Doe", "Software Developer"),
                new Employee(2, "Jane Smith", "Project Manager")
            };

        Method1();

        Console.WriteLine("TaskDelay Endpoint: End");
        return Ok(employees);
    }

    [HttpGet("ThreadSleep")]
    [Produces("application/json")]
    public ActionResult<IEnumerable<Employee>> Get2()
    {
        Console.WriteLine("ThreadSleep Endpoint: Begin");
        var employees = new List<Employee>
            {
                new Employee(1, "John Doe", "Software Developer"),
                new Employee(2, "Jane Smith", "Project Manager")
            };

        Method2();

        Console.WriteLine("ThreadSleep Endpoint: End");
        return Ok(employees);
    }
    /// <summary>
    /// Using Task.Delay
    /// </summary>
    /// <returns></returns>
    private async Task Method1()
    {
        Console.WriteLine("Method1: Begin");
        await Task.Delay(_sleepMiliseconds);
        Console.WriteLine("Method1: End");
    }

    /// <summary>
    /// Using Thread.Sleep
    /// </summary>
    /// <returns></returns>
    private async Task Method2()
    {
        Console.WriteLine("Method2: Begin");
        Thread.Sleep(_sleepMiliseconds);
        Console.WriteLine("Method2: End");
    }
}
public record Employee(int Id, string Name, string Position)
{

}

