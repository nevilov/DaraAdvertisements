using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Identity.Contracts.Exceptions
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(string message): base(message)
        {

        }       
    }
}
