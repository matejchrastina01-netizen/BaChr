// Abstraction
using Microsoft.AspNetCore.Http;

namespace UTB.BaChr.Mapy.Application.Abstraction
{
    public interface IFileUploadService
    {
        string Upload(IFormFile fileToUpload);
    }
}