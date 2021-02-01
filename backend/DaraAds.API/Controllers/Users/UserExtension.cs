using DaraAds.Core.Dto.Users;
using DaraAds.Core.Entities;

namespace DaraAds.API.Controllers.Users
{
    public static class UserExtension
    {
        public static UsersListItemDto ToDto(this User user)
        {
            return new UsersListItemDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Phone = user.Phone,
                Email = user.Email,
            };
        }
    }
}
