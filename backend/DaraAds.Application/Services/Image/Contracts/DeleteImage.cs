namespace DaraAds.Application.Services.Image.Contracts
{
    public static class DeleteImage
    {
        public sealed class Request
        {
            public string Id { get; set; }
        }
    }
}