using DaraAds.Domain.Shared;

namespace DaraAds.Domain
{    
     /// <summary>
     /// Сущность жалобы
     /// </summary>
    public class Abuse : MutableEntity<int>
    {

        /// <summary>
        /// ID User'a - автора жалобы (для отправки ему ув.)
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// ID объявления, на которое пожаловались
        /// </summary>
        public int AbuseAdvId { get; set; }

        /// <summary>
        /// Приоритет жалобы (повышается при частых жалобах)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Текст (суть) жалобы
        /// </summary>
        public string AbuseText { get; set; }

    }
}
