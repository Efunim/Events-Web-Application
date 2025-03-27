using Microsoft.AspNetCore.Http;

namespace Events.Infastructure.Data.Files
{
    public interface IFileManager
    {
        Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken);
        string GetFile(string fileName);
    }
}
