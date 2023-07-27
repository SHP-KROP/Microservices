using AuctionService.Application.Services.Abstractions;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace AuctionService.Infrastructure;

public sealed class BlobService : IBlobService
{
    private const string ContainerName = "online-auction";
    
    private readonly BlobServiceClient _client;

    public BlobService(BlobServiceClient client)
    {
        _client = client;
    }
    
    public async Task<(string fileName, Uri url)> UploadFile(IFormFile file, string fileName)
    {
        var container = _client.GetBlobContainerClient(ContainerName);
        var blob = container.GetBlobClient(fileName);
        
        await using var stream = file.OpenReadStream();
        
        await blob.UploadAsync(stream, true);

        return (file.FileName, blob.Uri);
    }
}