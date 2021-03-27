namespace DaraAds.API.Dto.Advertisement
{
    public sealed class GetAllAdvertisementRequest
    {
        /// <summary>
        /// Количество возвращаемых объявлений
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// Смещение начиная с котрого возвращаются объявления
        /// </summary>
        public int Offset { get; set; } = 0;
    }
}