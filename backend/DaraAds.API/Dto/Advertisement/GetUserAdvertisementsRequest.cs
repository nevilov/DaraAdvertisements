using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaraAds.Application.Common;

namespace DaraAds.API.Dto.Advertisement
{
    public class GetUserAdvertisementsRequest : PagedDto
    {
        /// <summary>
        /// Идентификатор пользователя  
        /// </summary>
        public string Id { get; set; }
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