using System;

namespace DaraAds.API.Dto.Users
{
    public class BlockUserRequest
    {
        public string UserId { get; set; }
        public DateTime UntilDate { get; set; }
    }
}
