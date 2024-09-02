using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JournalApi.Controllers;
[ApiController]
    [Route("[controller]")]
public class BlobStorageController:ControllerBase
{
    private readonly IConfiguration _configuration;

    public BlobStorageController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        BlobServiceClient blobServiceClient =
            new BlobServiceClient(_configuration.GetConnectionString("JournalApiBlobs"));
        var containerName = "pictures";
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
        var blobHttpHeadres = new BlobHttpHeaders();
        blobHttpHeadres.ContentType = file.ContentType;
        await blobClient.UploadAsync(file.OpenReadStream(),overwrite: false);
        return Ok();
    }
    [HttpGet("download")]
    public async Task<IActionResult> Download([FromQuery]string blobName)
    {
        BlobServiceClient blobServiceClient =
            new BlobServiceClient(_configuration.GetConnectionString("JournalApiBlobs"));
        var containerName = "pictures";
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        var downloadResponse=await blobClient.DownloadContentAsync();
        var content=downloadResponse.Value.Content.ToStream();
        var contentType = blobClient.GetProperties().Value.ContentType;
        return File(content, contentType,fileDownloadName: blobName);
    }
    
}