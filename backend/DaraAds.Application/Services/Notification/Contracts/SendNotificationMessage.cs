namespace DaraAds.Application.Services.Notification.Contracts
{
    public class SendNotificationMessage
    {
        public string RecipientEmail { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
