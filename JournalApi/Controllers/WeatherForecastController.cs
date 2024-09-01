using System.Net.Sockets;
using JournalApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace JournalApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _service;
    private readonly IMemoryCache _memoryCache;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _service = service;
        _memoryCache = memoryCache;
    }

    [HttpGet]
    [Route("{take:int}/example")]
    public async Task<ObjectResult> Get([FromQuery] int max, [FromRoute] int take)
    {
        var sepamphore = new SemaphoreSlim(1, 1);
        if (!_memoryCache.TryGetValue("forecast", out IEnumerable<WeatherForecast>? forecast))
        {
            try
            {
                await sepamphore.WaitAsync();
                if (!_memoryCache.TryGetValue("forecast", out forecast))
                {
                    forecast = _service.Get();
                    _memoryCache.Set("forecast", forecast, 
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10)));
                }
            }
            finally
            {
                sepamphore.Release();
            }
        }

        return Ok(forecast);
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromQuery] int take, [FromBody] TemperatureRequest request)
    {
        if (take < 0 || request.MaxTemperature < request.MinTemperature)
        {
            return BadRequest("Error");
        }

        var result = _service.ExerciseGet(request.MaxTemperature, request.MinTemperature, take);
        return Ok(result);
    }

    [HttpPost(Name = "AddWeatherForecast")]
    public ObjectResult Add([FromBody] WeatherForecast weatherForecast)
    {
        var weatherNewForPlace = new WeatherForecast
        {
            Date = weatherForecast.Date,
            TemperatureC = weatherForecast.TemperatureC,
            Summary = weatherForecast.Summary
        };
        return StatusCode(200, weatherNewForPlace);
    }
}

public class TemperatureRequest
{
    public int MinTemperature { get; set; }
    public int MaxTemperature { get; set; }
}