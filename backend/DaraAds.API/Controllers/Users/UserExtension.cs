using System.Linq;
using System.Security.Claims;
using DaraAds.API.Dto.Users;
using DaraAds.Domain;

namespace DaraAds.API.Controllers.Users
{
    public static class UserExtension
    {
        public static UsersListItemDto ToDto(this User user)
        {
            return new UsersListItemDto
            {
//                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Phone = user.Phone,
                Email = user.Email,
            };
        }

        public static UsersListItemDto ToDto(this ClaimsPrincipal principal)
        {
            return new()
            {
                Id = int.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
                Name = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value
            };
        }
    }
}
