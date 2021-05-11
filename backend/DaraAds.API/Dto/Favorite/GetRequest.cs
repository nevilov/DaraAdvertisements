using DaraAds.Application.Common;

namespace DaraAds.API.Dto.Favorite
{
    public sealed class GetRequest : PagedDto
    {
        /// <summary>
        /// Критерий сортировки
        /// </summary>
        /// <example>Id</example>
        public string SortBy { get; set; } = SortConstants.SortBy;
        /// <summary>
        /// Направление сортировки (asc, desc)
        /// </summary>
        /// <example>asc</example>
        public string SortDirection { get; set; } = SortConstants.SortDirection;
    }
}