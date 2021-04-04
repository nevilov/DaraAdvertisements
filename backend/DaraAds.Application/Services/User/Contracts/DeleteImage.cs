namespace DaraAds.Application.Services.User.Contracts
{
    public static class DeleteImage
    {
        public sealed class Request
        {
            public string ImageId { get; set; }
        }
    }
}