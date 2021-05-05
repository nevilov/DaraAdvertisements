using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Message.Contracts.Exceptions
{
    public class HaveNoRigthToSendMessageChat : NoRightsException
    {
        public HaveNoRigthToSendMessageChat(string message) : base(message)
        {
        }
    }
}
