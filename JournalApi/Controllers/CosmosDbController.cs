using Journal.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace JournalApi.Controllers;


[ApiController]
[Route("[controller]")]
public class CosmosDbController : ControllerBase
{
    private readonly IConfiguration _configuration;


    public CosmosDbController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Employee employee)
    {
        var container = await GetContainer();
        await container.CreateItemAsync(employee);
        return Accepted();
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string id, [FromQuery] string partitionKey)
    {
        var container = await GetContainer();
        var employee = await container.ReadItemAsync<Employee>(id, new PartitionKey(partitionKey));
        return Ok(employee);
    }
    [HttpGet("query-linq")]
    public async Task<IActionResult> QueryLinq()
    {
        var container = await GetContainer();
        var employess=container.GetItemLinqQueryable<Employee>(true).Where(e => e.department == "IT").ToList();
        return Ok(employess);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Employee employee)
    {
        var container = await GetContainer();
        var patchOperations = new List<PatchOperation>
        {
            PatchOperation.Replace("/name",employee.name),
            PatchOperation.Replace("/address",employee.address)
        };
        await container.PatchItemAsync<Employee>(
            id: employee.id,
            partitionKey: new PartitionKey(employee.department),
            patchOperations: patchOperations);
        return Accepted();
    }
    private async Task<Container> GetContainer()
    {
        CosmosClient client = new CosmosClient(_configuration.GetConnectionString("JournalApiCosmosDB"));
        Database database = await client.CreateDatabaseIfNotExistsAsync("Employess");

        return await database.CreateContainerIfNotExistsAsync("Employees","/department",400);
    }
    
}
