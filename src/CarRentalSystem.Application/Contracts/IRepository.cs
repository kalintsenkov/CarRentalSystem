namespace CarRentalSystem.Application.Contracts
{
    using Domain.Common;

    public interface IRepository<out TEntity>
        where TEntity : IAggregateRoot
    {
    }
}