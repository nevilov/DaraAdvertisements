namespace DaraAds.Domain.Shared.Exceptions
{
    public abstract class DuplicateException : DomainException
    {
        protected DuplicateException(string message) : base(message)
        {
        }
    }
}
