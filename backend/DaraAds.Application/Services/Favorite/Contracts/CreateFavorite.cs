namespace DaraAds.Application.Services.Favorite.Contracts
{
    public static class CreateFavorite
    {
        public class Request
        {
            public int AdvertisementId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
        }
    }
}
