namespace DaraAds.API.Dto.Users
{
    public class ResetPasswordRequest
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public string NewPassword { get; set; }

        //TODO провалидировать!
        public string RepeatedPassword { get; set; }
    }
}
