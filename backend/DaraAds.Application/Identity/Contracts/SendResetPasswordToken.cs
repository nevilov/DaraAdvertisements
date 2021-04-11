using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaraAds.Application.Identity.Contracts
{
    public static class SendResetPasswordToken
    {
        public class Request
        {
            public string Email { get; set; }
        }

        public class Response
        {
            public bool IsSuccess { get; set; }
            public string UserId { get; set; }
            public string[] Errors { get; set; }
        }
    }
}
