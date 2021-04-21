namespace DaraAds.API.Dto.Message
{
    public class SendMessageRequest
    {
        public long ChatId { get; set; }
        public string RecipientId { get; set; }
        public string Text { get; set; }    
    }
}
