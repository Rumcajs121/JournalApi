using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using log4net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JournalApi.Controllers;
[ApiController]
    [Route("[controller]")]
public class BlobStorageController:ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILog _log;

    public BlobStorageController(IConfiguration configuration, ILog log)
    {
        _configuration = configuration;
        _log = log;
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
        
        _log.Info("Everything is ok");
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
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery]string blobName)
    {
        BlobServiceClient blobServiceClient =
            new BlobServiceClient(_configuration.GetConnectionString("JournalApiBlobs"));
        var containerName = "pictures";
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        if (await blobClient.ExistsAsync())
        {
            var deleteBlob=await blobClient.DeleteAsync();
            return Ok(blobName);
        }
        return NotFound($"Blob {blobName} is not find");
    }
    
}