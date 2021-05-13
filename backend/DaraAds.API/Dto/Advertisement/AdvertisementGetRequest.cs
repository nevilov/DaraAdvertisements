using System;
using DaraAds.Application.Common;

namespace DaraAds.API.Dto.Advertisement
{
    public sealed class AdvertisementGetRequest : PagedDto
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
        
        /// <summary>
        /// Строка для поиска объявлений по названию или описанию
        /// </summary>
        public string SearchString { get; set; }
        
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }
    
        /// <summary>
        /// Минимальная цена
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Максимальная цена
        /// </summary>
        public decimal MaxPrice { get; set; } = AdvertisementConstants.MaxPrice;

        /// <summary>
        /// Минимальная дата 
        /// </summary>
        public DateTime MinDate { get; set; }

        /// <summary>
        /// Максимальная дата 
        /// </summary>
        public DateTime MaxDate { get; set; } = DateTime.UtcNow.Date;
    }
}