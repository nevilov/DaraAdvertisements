using System.Collections.Generic;
using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{    /// <summary>
     /// Сущность Категория
     /// </summary>
    public class Category : MutableEntity<int>
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Id родителя категории
        /// </summary>
        public int? ParentCategoryId { get; set; }

        /// <summary>
        /// Родительская категория
        /// </summary>
        public virtual Category ParentCategory { get; set; }
        
        /// <summary>
        /// Список подкатегорий
        /// </summary>
        public virtual IList<Category> ChildCategories { get; set; }
    }
}