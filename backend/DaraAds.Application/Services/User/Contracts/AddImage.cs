using Microsoft.AspNetCore.Http;

namespace DaraAds.Application.Services.User.Contracts
{
    public static class AddImage
    {
        public sealed class Request
        {
            public IFormFile Image { get; set; }
            
        }
    }
}