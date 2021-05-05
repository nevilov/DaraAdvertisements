namespace DaraAds.Domain.Shared.Exceptions
{
    public abstract class NoRightsException : DomainException
    {
        protected NoRightsException(string message) : base(message)
        {
        }
    }
}