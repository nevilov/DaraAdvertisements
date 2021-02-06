namespace DaraAds.Domain.Shared.Exceptions
{
    public abstract class NoRightException : DomainException
    {
        protected NoRightException(string message) : base(message)
        {
        }
    }
}