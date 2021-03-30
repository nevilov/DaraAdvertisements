namespace DaraAds.API.Dto.Users
{
    public sealed class UserRegisterRequest
    {
        public string Username { get; set; }
        
        public string Name { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string Phone { get; set; }
        
        public string Password { get; set; }
    }
}