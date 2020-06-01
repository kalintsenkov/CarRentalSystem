#nullable enable
namespace CarRentalSystem.Application.Features.CarAds.Queries.Search
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Specifications.CarAds;
    using MediatR;

    public class SearchCarAdsQuery : IRequest<SearchCarAdsOutputModel>
    {
        public string? Manufacturer { get; set; }

        public int? Category { get; set; }

        public decimal? MinPricePerDay { get; set; }

        public decimal? MaxPricePerDay { get; set; }

        public class SearchCarAdsQueryHandler : IRequestHandler<SearchCarAdsQuery, SearchCarAdsOutputModel>
        {
            private readonly ICarAdRepository carAdRepository;

            public SearchCarAdsQueryHandler(ICarAdRepository carAdRepository)
                => this.carAdRepository = carAdRepository;

            public async Task<SearchCarAdsOutputModel> Handle(
                SearchCarAdsQuery request,
                CancellationToken cancellationToken)
            {
                var carAdSpecification = new CarAdByManufacturerSpecification(request.Manufacturer)
                    .And(new CarAdByCategorySpecification(request.Category))
                    .And(new CarAdByPricePerDaySpecification(request.MinPricePerDay, request.MaxPricePerDay));

                var carAdListings = await this.carAdRepository.GetCarAdListings(carAdSpecification, cancellationToken);
                var totalCarAds = await this.carAdRepository.Total(cancellationToken);

                return new SearchCarAdsOutputModel(carAdListings, totalCarAds);
            }
        }
    }
}
