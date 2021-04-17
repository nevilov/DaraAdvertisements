using System.Collections.Generic;

namespace DaraAds.Application.Identity.Contracts
{
    public static class ResetUserPassword
    {
        public class Request
        {
            public string UserId { get; set; }
            public string Token { get; set; }
            public string NewPassword { get; set; }
        }

        public class Response
        {
            public bool isSuccess { get; set; }
            public List<string> Errors { get; set; }
        }
    }
}
