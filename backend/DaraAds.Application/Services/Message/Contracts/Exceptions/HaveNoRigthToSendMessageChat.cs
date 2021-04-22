using DaraAds.Domain.Shared.Exceptions;

namespace DaraAds.Application.Services.Message.Contracts.Exceptions
{
    public class HaveNoRigthToSendMessageChat : NoRightException
    {
        public HaveNoRigthToSendMessageChat(string message) : base(message)
        {
        }
    }
}
