namespace DaraAds.API.Dto.Users
{
    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; }
        
        public string RepeatedNewPassword { get; set; }

        public string OldPassword { get; set; }
    }
}