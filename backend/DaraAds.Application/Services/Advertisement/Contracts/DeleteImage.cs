namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class DeleteImage
    {
        public sealed class Request
        {
            public int Id { get; set; }
            
            public string ImageId { get; set; }
        }
    }
}