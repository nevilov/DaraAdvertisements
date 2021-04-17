using System;
using DaraAds.Application.Common;

namespace DaraAds.API.Dto.Advertisement
{
    public sealed class AdvertisementGetRequest : PagedDto
    {
        public AdvertisementGetRequest()
        {
            OrderBy = AdvertisementConstants.SortOrderByIdAsc;
        }
        
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