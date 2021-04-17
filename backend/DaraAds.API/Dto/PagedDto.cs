using DaraAds.Application.Common;

namespace DaraAds.API.Dto
{
    public class PagedDto
    {
        /// <summary>
        /// Количество возвращаемых объявлений
        /// </summary>
        public int Limit { get; set; } = PagedConstants.PaginationLimit;

        /// <summary>
        /// Смещение, начиная с котрого возвращаются объявления
        /// </summary>
        public int Offset { get; set; } = PagedConstants.PaginationOffset;
        
        /// <summary>
        /// Критерий сортировки
        /// </summary>
        /// <example>Id</example>
        public string OrderBy { get; set; }
    }
}