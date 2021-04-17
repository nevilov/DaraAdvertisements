using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{
    public class Favorite : MutableEntity<int>
    {
        /// <summary>
        /// Избранное объявления
        /// </summary>
        public virtual Advertisement Advertisement { get; set; }
        
        /// <summary>
        /// Уникальный идентификатор избранного объявления
        /// </summary>
        public int AdvertisementId { get; set; }

        /// <summary>
        /// Пользователь, которому пренадлежит это объявление
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя, кокоторому пренадлежит это объявление
        /// </summary>
        public string UserId { get; set; }
    }
}
