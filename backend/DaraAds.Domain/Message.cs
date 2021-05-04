using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{
    public class Message : BaseEntity<long>
    {
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public virtual User Sender { get; set; }

        /// <summary>
        /// Идентификатор отправителя
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public virtual User Recipient { get; set; }

        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public string RecipientId { get; set; }

        /// <summary>
        /// Чат, которому пренадлежит сообщение
        /// </summary>
        public virtual Chat Chat { get; set; }

        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public long ChatId { get; set; }
    }
}
