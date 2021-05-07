using DaraAds.Domain.Shared.Exceptions;
namespace DaraAds.Application.Services.Message.Contracts.Exceptions
{
    public class HaveNoRigthToGetMessageByChatException : NoRightsException
    {
        public HaveNoRigthToGetMessageByChatException(string message) : base(message)
        {
        }
    }
}
