namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using Application.Contracts;
    using Domain.Common;

    internal abstract class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        private readonly CarRentalDbContext db;

        protected DataRepository(CarRentalDbContext db) => this.db = db;

        protected IQueryable<TEntity> All() => this.db.Set<TEntity>();
    }
}