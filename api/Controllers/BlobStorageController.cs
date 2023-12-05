using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class BlobStorageController : ControllerBase
{
    [HttpPost("/uploadfile")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        BlobServiceClient blobServiceClient =
            new BlobServiceClient(Environment.GetEnvironmentVariable("blobconnectionstring"));
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("products");

        using (var stream = file.OpenReadStream())
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(stream, true);
        }
        return Ok();
    }

    [HttpGet("/getimage")]
    public async Task<IActionResult> GetImage(string blobUri)
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(blobUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();

                return File(content, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
        
    }
}