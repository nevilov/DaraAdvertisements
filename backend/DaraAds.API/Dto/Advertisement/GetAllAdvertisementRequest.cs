using DaraAds.Application.Common;

namespace DaraAds.API.Dto.Advertisement
{
    public sealed class GetAllAdvertisementRequest : PagedDto
    {
        /// <summary>
        /// Критерий сортировки, по умолчанию по Id
        /// </summary>
        public string SortOrder { get; set; } = AdvertisementConstants.SortOrderByIdAsc;

        /// <summary>
        /// Строка для поиска объявлений по названию или описанию
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// Идентификатор категории для фильтрации объявлений
        /// </summary>
        public int CategoryId { get; set; }
    }
}