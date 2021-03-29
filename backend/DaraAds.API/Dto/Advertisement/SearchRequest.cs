namespace DaraAds.API.Dto.Advertisement
{
    public class SearchRequest
    {
        public string KeyWord { get; set; }

        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 10;

    }
}