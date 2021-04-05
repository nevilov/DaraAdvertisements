using Microsoft.AspNetCore.Http;

namespace DaraAds.Application.Services.Image.Contracts
{
    public static class UploadImage
    {
        public sealed class Request
        {
            public IFormFile Image { get; set; }
        }
        
        public sealed class Response
        {
            public string Id { get; set; }
        }
    }
}