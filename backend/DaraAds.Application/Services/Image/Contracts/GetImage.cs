namespace DaraAds.Application.Services.Image.Contracts
{
    public static class GetImage
    {
        public sealed class Request
        {
            public string Id { get; set; }
        }

        public sealed class Response
        {
            public string ImageUrl { get; set; }
            
            public string ImageBlob { get; set; }
        }
    }
}