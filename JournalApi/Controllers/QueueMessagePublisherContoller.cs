using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using log4net;
namespace JournalApi.Controllers;

[ApiController]
[Route("[controller]")]
public class QueueMessagePublisherContoller : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILog _log;

    public QueueMessagePublisherContoller(IConfiguration configuration, ILog log)
    {
        _configuration = configuration;
        _log = log;
    }
    [HttpPost]
    public async Task<IActionResult> Publish(WeatherForecast returnetForecast)
    {
        var queueName = "returns";
        QueueClient queueClient = new QueueClient(_configuration.GetConnectionString("JournalApiBlobs"), queueName);
        await queueClient.CreateIfNotExistsAsync();
        var serializedMessage = JsonConvert.SerializeObject(returnetForecast);
        queueClient.SendMessageAsync(serializedMessage);
        _log.Info($"Everything is on I send for destination queue: {queueName}. This is this message:{serializedMessage}");
        return Ok(); 
    }
    
}