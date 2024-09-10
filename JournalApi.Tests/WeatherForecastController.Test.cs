using FakeItEasy;
using JournalApi.Controllers;
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

    [Fact]
    public void GenerateRequest_StatusCodeAction_ReturnStatusOk()
    {
        int take = 3;

        var fakeRequestTemperature = new TemperatureRequest
        {
            MinTemperature = 10,
            MaxTemperature = 35
        };
        var fakeWeatherForecast = A.CollectionOfFake<WeatherForecast>(take).AsEnumerable();
        var logger = A.Fake<ILogger<JournalApi.Controllers.WeatherForecastController>>();
        var service = A.Fake<IWeatherForecastService>();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        A.CallTo(() => service.ExerciseGet(fakeRequestTemperature.MaxTemperature, fakeRequestTemperature.MinTemperature, take))
            .Returns(fakeWeatherForecast);
        var controller=new Controllers.WeatherForecastController(logger,service,memoryCache);
        var result = controller.Generate(take, fakeRequestTemperature);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200,okResult.StatusCode);
        var returnedValue = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(okResult.Value);
        Assert.Equal(fakeWeatherForecast, returnedValue);
    }
}

