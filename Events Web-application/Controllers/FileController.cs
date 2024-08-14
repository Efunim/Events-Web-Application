using Events.Infastructure;
using Events.Infastructure.Data.Files;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileController(IFileManager fileManager) : ControllerBase
    {
        [HttpGet(Name = "GetFile")]
        public IActionResult GetFile(string fileName)
        {
            var filePath = fileManager.GetFile(fileName);

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "image/jpeg");
        }

        [HttpPost(Name = "UploadImage")]
        public async Task<string> UploadImage(IFormFile image, CancellationToken cancellationToken) =>
            await fileManager.UploadImageAsync(image, cancellationToken);
    }
}
