using System.Net.Sockets;
using JournalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JournalApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("{take:int}/example")]
    public ObjectResult Get([FromQuery] int max, [FromRoute] int take)
    {
        var result=_service.Get();
        return Ok(result);

    }
    [HttpPost("generate")]
    public IActionResult Generate([FromQuery] int take,[FromBody]TemperatureRequest request)
    {
        if (take < 0 || request.MaxTemperature < request.MinTemperature)
        {
            return BadRequest("Error");
        }
        var result=_service.ExerciseGet(request.MaxTemperature,request.MinTemperature,take);
        return Ok(result);
        

    }
    [HttpPost(Name="AddWeatherForecast")]
    public ObjectResult Add ([FromBody] WeatherForecast weatherForecast)
    {
        var weatherNewForPlace = new WeatherForecast
        {
            Date = weatherForecast.Date,
            TemperatureC = weatherForecast.TemperatureC,
            Summary = weatherForecast.Summary
        };
        return StatusCode(200,weatherNewForPlace);
    }

}

public class TemperatureRequest
{
    public int MinTemperature { get; set; }
    public int MaxTemperature { get; set; }
}
