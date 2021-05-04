using DaraAds.Domain.Shared;
using System.Collections.Generic;

namespace DaraAds.Domain
{
    /// <summary>
    /// Сущность пользователь
    /// </summary>
    public class User : MutableEntity<string>
    {
        /// <summary>
        /// Никнейм юзера
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Аватар пользователя
        /// </summary>
        public string Avatar { get; set; }
        
        /// <summary>
        /// Список картинок
        /// </summary>
        public virtual ICollection<Image> Images { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Корпоративный аккаунт
        /// </summary>
        public bool IsCorporation { get; set; }

        /// <summary>
        /// Подписка на уведомления
        /// </summary>
        public bool IsSubscribedToNotifications { get; set; }

        /// <summary>
        /// Избранные объявления пользователя
        /// </summary>
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
