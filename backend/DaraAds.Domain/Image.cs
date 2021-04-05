using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{
    /// <summary>
    /// Сущность картинка
    /// </summary>
    public class Image : MutableEntity<string>
    {
        /// <summary>
        /// Уникальное имя картинки
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Картинка в blob формате
        /// </summary>
        public byte[] ImageBlob { get; set; }
        
        /// <summary>
        /// Id объявления, которому принадлежит картинка
        /// </summary>
        public int? AdvertisementId { get; set; }
        
        /// <summary>
        /// Навигационное свойство объявление
        /// </summary>
        public virtual Advertisement Advertisement { get; set; }
        
        /// <summary>
        /// Id пользователя, которому принадлежит картинка
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Навигационное свойство пользователь
        /// </summary>
        public virtual User User { get; set; }
    }
}