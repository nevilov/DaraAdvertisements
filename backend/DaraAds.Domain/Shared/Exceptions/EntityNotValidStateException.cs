namespace DaraAds.Domain.Shared.Exceptions
{
    public abstract class EntityNotValidStateException : DomainException
    {
        protected EntityNotValidStateException(string message) : base(message)
        {
        }
    }
}