namespace DaraAds.Domain.Dto.Advertisement
{
    public class AdvertisementDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Cover { get; set; }

        public int UserId { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }
    }
}
