namespace DaraAds.Application.Identity.Contracts
{
    public static class ChangePassword
    {
        public class Request
        {
            public string NewPassword { get; set; }
            
            public string OldPassword { get; set; }
        }
    }
}