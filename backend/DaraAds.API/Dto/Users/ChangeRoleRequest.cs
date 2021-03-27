namespace DaraAds.API.Dto.Users
{
    public class ChangeRoleRequest
    {
        public string Email { get; set; }

        public string NewRole { get; set; }
    }
}