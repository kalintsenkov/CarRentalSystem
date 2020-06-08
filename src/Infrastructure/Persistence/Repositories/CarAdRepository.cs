namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.CarAds;
    using Application.Features.CarAds.Queries.Categories;
    using Application.Features.CarAds.Queries.Details;
    using Application.Features.CarAds.Queries.Search;
    using AutoMapper;
    using Domain.Models.CarAds;
    using Domain.Specifications;
    using Microsoft.EntityFrameworkCore;

    internal class CarAdRepository : DataRepository<CarAd>, ICarAdRepository
    {
        private readonly IMapper mapper;

        public CarAdRepository(CarRentalDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var carAd = await this.Data.CarAds.FindAsync(id);
            if (carAd == null)
            {
                return false;
            }

            this.Data.CarAds.Remove(carAd);
            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
            Specification<CarAd> specification,
            CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<CarAdListingModel>(this
                    .AllAvailable()
                    .Where(specification))
                .ToListAsync(cancellationToken);

        public async Task<DetailsCarAdOutputModel> GetDetails(
            int id,
            CancellationToken cancellationToken = default)
            => await this.mapper
                .ProjectTo<DetailsCarAdOutputModel>(this
                    .AllAvailable()
                    .Where(c => c.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Category> GetCategory(
            int categoryId,
            CancellationToken cancellationToken = default)
            => await this.Data
                .Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        public async Task<Manufacturer> GetManufacturer(
            string manufacturerName,
            CancellationToken cancellationToken = default)
            => await this.Data
                .Manufacturers
                .FirstOrDefaultAsync(c => c.Name == manufacturerName, cancellationToken);

        public async Task<int> Total(CancellationToken cancellationToken = default)
            => await this
                .AllAvailable()
                .CountAsync(cancellationToken);

        public async Task<IEnumerable<CategoriesCarAdsOutputModel>> GetCarAdCategories(
            CancellationToken cancellationToken = default)
        {
            var categories = await this.mapper
                .ProjectTo<CategoriesCarAdsOutputModel>(this.Data.Categories)
                .ToDictionaryAsync(k => k.Id, cancellationToken);

            var carAdsPerCategory = await this.AllAvailable()
                .GroupBy(c => c.Category.Id)
                .Select(gr => new
                {
                    CategoryId = gr.Key,
                    TotalCarAds = gr.Count()
                })
                .ToListAsync(cancellationToken);

            carAdsPerCategory.ForEach(c => categories[c.CategoryId].TotalCarAds = c.TotalCarAds);

            return categories.Values;
        }

        private IQueryable<CarAd> AllAvailable()
            => this
                .All()
                .Where(car => car.IsAvailable);
    }
}
