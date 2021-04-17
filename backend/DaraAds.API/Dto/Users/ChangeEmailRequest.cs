namespace DaraAds.API.Dto.Users
{
    public class ChangeEmailRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewEmail { get; set; }
    }
}