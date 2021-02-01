using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Core.Entities
{
    /// <summary>
    /// Сущность объявление
    /// </summary>
    public class Advertisement : BaseEntity
    {
        /// <summary>
        /// Заголовок объявления
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание объявления
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Обложка объявления
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// Автор объявления
        /// </summary>
        public User OwnerUser { get; set; }

        /// <summary>
        /// Код категории объявления
        /// </summary>
        public string Category { get; set; }

        public string SubCategory { get; set; }

    }
}
