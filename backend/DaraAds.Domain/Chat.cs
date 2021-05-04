using DaraAds.Domain.Shared;
using System.Collections.Generic;

namespace DaraAds.Domain
{
    public class Chat : MutableEntity<long>
    {
        /// <summary>
        /// Объявление, по которому ведется чат
        /// </summary>
        public virtual Advertisement Advertisement { get; set; }

        /// <summary>
        /// Покупатель
        /// </summary>
        public virtual User Buyer { get; set; }

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public string BuyerId { get; set; }

        /// <summary>
        /// Коллекция сообщений чата
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }
    }
}
