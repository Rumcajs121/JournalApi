using Azure.Storage.Queues;
using JournalApi;

Console.WriteLine("Hello, World!");
var queueName = "returns";

QueueClient queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=journalapisane;AccountKey=gTxHF4SrD7Ru7KJj7GiOxXjt50LetHOAzjrxsOJK/jSrMaSuwmM5ck1quZxtpifIu4SUY3Uh2SQ++AStHMhu0g==;EndpointSuffix=core.windows.net", queueName);
while (true)
{
    var message = queueClient.ReceiveMessage();
    if (message.Value != null)
    {
        var response=message.Value.Body.ToObjectFromJson<WeatherForecast>();
        Process(response);
        await queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
    }
}

void Process(WeatherForecast forecast)
{
    Console.WriteLine($"{forecast.Date}, {forecast.TemperatureC}, {forecast.TemperatureF} , {forecast.Summary}");
}