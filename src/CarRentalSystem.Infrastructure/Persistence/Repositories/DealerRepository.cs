namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Dealers;
    using Domain.Models.Dealers;

    internal class DealerRepository : DataRepository<Dealer>, IDealerRepository
    {
        public DealerRepository(CarRentalDbContext db) 
            : base(db)
        {
        }

        public async Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken = default) 
            => await this.Data.Dealers.FindAsync(userId, cancellationToken);
    }
}