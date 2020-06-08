namespace CarRentalSystem.Domain.Exceptions
{
    public class InvalidCategoryException : BaseDomainException
    {
        public InvalidCategoryException()
        {
        }

        public InvalidCategoryException(string message) => this.Message = message;
    }
}