namespace DaraAds.Application.Services.Advertisement.Contracts
{
    public class ImportExcelMessage
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string OwnerId { get; set; }

        public int CategoryId { get; set; }

        
    }
}
