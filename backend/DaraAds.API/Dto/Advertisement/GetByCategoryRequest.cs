namespace DaraAds.API.Dto.Advertisement
{
    public sealed class GetByCategoryRequest : PagedDto
    {
        public int CategoryId { get; set;}
    }
}