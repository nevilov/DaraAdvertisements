using System.ComponentModel.DataAnnotations;;

namespace DaraAds.API.Dto.Users
{
    public sealed class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
