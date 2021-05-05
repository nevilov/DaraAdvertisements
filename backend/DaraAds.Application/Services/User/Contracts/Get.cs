using System;
namespace DaraAds.Application.Services.User.Contracts
{
    public static class Get
    {
        public class Request
        {
            public string Id { get; set; }
            public bool isCurrent { get; set; }
        }

        public class Response
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string Phone { get; set; }
            public string Avatar { get; set; }
            public bool IsSubscribedToNotifications { get; set; }
            public bool IsCorporation { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}
