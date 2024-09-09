using FakeItEasy;
using JournalApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace JournalApi.Tests;

public class WeatherForecastController
{
    [Fact]
    public async Task GetResult_StatusCodeGetAction_ReturnStatusOk()
    {
        //Arrage
        int max = 5;
        int take = 2;
        var fakeWeatherForecast = A.CollectionOfFake<WeatherForecast>(take).AsEnumerable();
        var logger = A.Fake<ILogger<JournalApi.Controllers.WeatherForecastController>>();
        var service = A.Fake<IWeatherForecastService>();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var semaphore = new SemaphoreSlim(1, 1);
        var cacheKey = "forecast";
        
        A.CallTo(() => service.Get()).Returns(fakeWeatherForecast.ToArray());
        var controller = new Controllers.WeatherForecastController(logger,service,memoryCache);
        var result = await controller.Get(max, take);


        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedValue = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(okResult.Value);
        Assert.Equal(fakeWeatherForecast, returnedValue);

    }
}

// [HttpGet]
// [Route("{take:int}/example")]
// public async Task<ObjectResult> Get([FromQuery] int max, [FromRoute] int take)
// {
//     var sepamphore = new SemaphoreSlim(1, 1);
//     if (!_memoryCache.TryGetValue("forecast", out IEnumerable<WeatherForecast>? forecast))
//     {
//         try
//         {
//             await sepamphore.WaitAsync();
//             if (!_memoryCache.TryGetValue("forecast", out forecast))
//             {
//                 forecast = _service.Get();
//                 _memoryCache.Set("forecast", forecast, 
//                     new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10)));
//             }
//         }
//         finally
//         {
//             sepamphore.Release();
//         }
//     }
//
//     return Ok(forecast);
// }