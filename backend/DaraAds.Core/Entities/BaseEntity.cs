using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Core.Entities
{
    /// <summary>
    /// Базовый класс для всех сущностей
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата создания сущности
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата обновления сущности, по умолчанию - null
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Дата удаления сущности, по умолчанию - null
        /// </summary>
        public DateTime? RemovedDate { get; set; }
    }
}
