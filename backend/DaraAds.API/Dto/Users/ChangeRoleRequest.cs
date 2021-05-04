namespace DaraAds.API.Dto.Users
{
    public class ChangeRoleRequest
    {
        public string UserId { get; set; }

        public string NewRole { get; set; }
    }
}