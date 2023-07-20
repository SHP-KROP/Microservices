using Microsoft.AspNetCore.Http;

namespace AuctionService.Application.Services.Abstractions;

public interface IBlobService
{
    public Task<(string fileName, Uri url)> UploadFile(IFormFile file, string fileName);
}