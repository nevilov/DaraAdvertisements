using System.ComponentModel.DataAnnotations;

namespace DaraAds.Domain.Dto.Users.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
