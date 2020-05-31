namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.CarAds;
    using Application.Features.CarAds.Queries.Search;
    using Domain.Models.CarAds;
    using Microsoft.EntityFrameworkCore;

    internal class CarAdRepository : DataRepository<CarAd>, ICarAdRepository
    {
        public CarAdRepository(CarRentalDbContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
            string? manufacturer = default,
            CancellationToken cancellationToken = default)
        {
            var query = this.AllAvailable();

            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                query = query
                    .Where(car => EF
                        .Functions
                        .Like(car.Manufacturer.Name, $"%{manufacturer}%"));
            }

            return await query
                .Select(car => new CarAdListingModel(
                    car.Id,
                    car.Manufacturer.Name,
                    car.Model,
                    car.ImageUrl,
                    car.Category.Name,
                    car.PricePerDay))
                .ToListAsync(cancellationToken);
        }

        public async Task<int> Total(CancellationToken cancellationToken = default)
            => await this
                .AllAvailable()
                .CountAsync(cancellationToken);

        private IQueryable<CarAd> AllAvailable()
            => this
                .All()
                .Where(car => car.IsAvailable);
    }
}
