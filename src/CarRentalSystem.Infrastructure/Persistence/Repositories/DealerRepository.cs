namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Dealers;
    using Application.Features.Dealers.Queries.Details;
    using AutoMapper;
    using Domain.Models.Dealers;
    using Microsoft.EntityFrameworkCore;

    internal class DealerRepository : DataRepository<Dealer>, IDealerRepository
    {
        private readonly IMapper mapper;

        public DealerRepository(CarRentalDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<DealerDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<DealerDetailsOutputModel>(this
                    .Data.Dealers
                    .Where(d => d.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken = default)
            => await this.Data.Dealers.FindAsync(userId, cancellationToken);
    }
}