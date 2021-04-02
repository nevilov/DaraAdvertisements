using Microsoft.AspNetCore.Http;

namespace DaraAds.Application.Services.Image.Contracts
{
    public static class Upload
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