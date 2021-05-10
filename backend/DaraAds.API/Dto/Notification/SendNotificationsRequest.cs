using System;
namespace DaraAds.API.Dto.Notification
{
    public class SendNotificationsRequest
    {
        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
