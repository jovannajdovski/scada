using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.model;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
        
    }
    [HttpGet("random", Name = "GetRandomWeatherForecast")]
    public object GetRandom()
    {
        object lockObject=new object();
        ScadaDBContext scadaDBContext = new ScadaDBContext();
        IOAdress adress = new IOAdress(1, "double", "0505");
        
        
        scadaDBContext.Adresses.Add(adress);
        scadaDBContext.SaveChanges();
        //SimulationDriver.SimulationDriver simulationDriver = new SimulationDriver.SimulationDriver(lockObject);
        //simulationDriver.SimulateWaterFillingAsync();
        return null;
    }
}
