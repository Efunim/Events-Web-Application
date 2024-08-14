using Events.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Infastructure.Data.Files
{
    public class FileManager : IFileManager
    {
        private readonly string _uploadsDirectory = $"{Directory.GetCurrentDirectory()}\\uploads";
        private readonly string _imagePath = $"images";

        public async Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentNullException("Error recieving file");

            if (!image.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Uploaded file is not image");

            var destinationDirectory = Path.Combine(_uploadsDirectory, _imagePath);
            Directory.CreateDirectory(destinationDirectory);

            var uniqueName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(destinationDirectory, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream, cancellationToken);
            }

            return $"{_imagePath}\\{uniqueName}";
        }

        public string GetFile(string fileName)
        {
            var directory = Path.Combine(_uploadsDirectory, fileName);
            if (!File.Exists(directory))
            {
                throw new ObjectNotFoundException($"File {fileName} wasn't found");
            }

            return directory;
        }
    }
}
