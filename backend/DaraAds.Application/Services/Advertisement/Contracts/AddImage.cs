using Microsoft.AspNetCore.Http;

namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class AddImage
    {
        public sealed class Request
        {
            public int Id { get; set; }
            
            public IFormFile Image { get; set; }
            
        }
    }
}