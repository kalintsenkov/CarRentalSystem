namespace CarRentalSystem.Domain.Exceptions;

public class InvalidCategoryException : BaseDomainException
{
    public InvalidCategoryException()
    {
    }

    public InvalidCategoryException(string error) => this.Error = error;
}