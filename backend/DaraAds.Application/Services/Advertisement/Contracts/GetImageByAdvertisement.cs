namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public static class GetImageByAdvertisement
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }

        public sealed class Response
        {
            public string ImageUrl { get; set; }

            public string ImageBlob { get; set; }
        }
    }
}