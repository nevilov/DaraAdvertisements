using System;
namespace DaraAds.API.Dto.Users
{
    public class ChangeUserCorporationStatus
    {
        public string UserId { get; set; }

        public bool isCorporation { get; set; }
    }
}
