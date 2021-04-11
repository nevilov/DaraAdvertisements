using System.Collections.Generic;

namespace DaraAds.Application.Identity.Contracts
{
    public static class ConfirmChangeEmail
    {
        public class Request
        {
            public string UserId { get; set; }
            public string Token { get; set; }
            public string NewEmail { get; set; }
        }

        public class Response
        {
            public bool isSuccess { get; set; }
            public List<string> Errors { get; set; }
        }
    }
}