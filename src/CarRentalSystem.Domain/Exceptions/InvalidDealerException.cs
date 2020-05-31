namespace CarRentalSystem.Domain.Exceptions
{
    public class InvalidDealerException : BaseDomainException
    {
        public InvalidDealerException()
        {
        }

        public InvalidDealerException(string message) => this.Message = message;
    }
}