namespace DaraAds.Domain.Shared.Exceptions
{
    public abstract class BlockedException : DomainException
    {
        protected BlockedException(string message) : base(message)
        {
        }
    }
}
