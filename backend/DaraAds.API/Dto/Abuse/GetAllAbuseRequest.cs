namespace DaraAds.API.Dto.Abuse
{
    public sealed class GetAllAbuseRequest
    {
        /// <summary>
        /// Количество возвращаемых жалоб
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// Смещение с которого возвращаются жалобы
        /// </summary>
        public int Offset { get; set; } = 0;
    }
}