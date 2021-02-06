using System.ComponentModel.DataAnnotations;

namespace DaraAds.Domain.Dto.Requests
{
    public sealed class UserRegisterRequest
    {
        [Required(ErrorMessage = "Имя пользователя - обязательно")]
        public string Name { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Email пользователя - обязательно")]
        public string Email { get; set; }

        public string Phone { get; set; }

        [MaxLength(30)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
