// Implementation
using Microsoft.AspNetCore.Http;
using System.IO;
using UTB.BaChr.Mapy.Application.Abstraction;

namespace UTB.BaChr.Mapy.Application.Implementation
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string RootPath; // Cesta k wwwroot

        public FileUploadService(string rootPath)
        {
            RootPath = rootPath;
        }

        public string Upload(IFormFile fileToUpload)
        {
            if (fileToUpload == null || fileToUpload.Length == 0) return null;

            string fileName = Path.GetFileNameWithoutExtension(fileToUpload.FileName);
            string extension = Path.GetExtension(fileToUpload.FileName);
            string uniqueName = $"{fileName}-{Guid.NewGuid()}{extension}";

            string pathSave = Path.Combine(RootPath, "img", "uploads");
            if (!Directory.Exists(pathSave))
            {
                Directory.CreateDirectory(pathSave);
            }

            string filePath = Path.Combine(pathSave, uniqueName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                fileToUpload.CopyTo(stream);
            }

            return Path.Combine("/img/uploads", uniqueName).Replace("\\", "/");
        }
    }
}