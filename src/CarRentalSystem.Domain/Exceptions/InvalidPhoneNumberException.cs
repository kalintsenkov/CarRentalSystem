namespace CarRentalSystem.Domain.Exceptions
{
    public class InvalidPhoneNumberException : BaseDomainException
    {
        public InvalidPhoneNumberException()
        {
        }

        public InvalidPhoneNumberException(string message) => this.Message = message;
    }
}