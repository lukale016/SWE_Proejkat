using Microsoft.AspNetCore.Http;

namespace Projekat.Models
{
    public class ImageUploadModel
    {
        public IFormFile Image { get; set; }
    }
}