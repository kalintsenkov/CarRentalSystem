namespace CarRentalSystem.Application.Features.CarAds.Queries.Search;

using System.Threading;
using System.Threading.Tasks;
using Domain.Models.CarAds;
using Domain.Specifications;
using Domain.Specifications.CarAds;
using MediatR;

public class SearchCarAdsQuery : IRequest<SearchCarAdsOutputModel>
{
    public string? Manufacturer { get; init; }

    public int? Category { get; init; }

    public decimal? MinPricePerDay { get; init; }

    public decimal? MaxPricePerDay { get; init; }

    public class SearchCarAdsQueryHandler : IRequestHandler<SearchCarAdsQuery, SearchCarAdsOutputModel>
    {
        private readonly ICarAdRepository carAdRepository;

        public SearchCarAdsQueryHandler(ICarAdRepository carAdRepository)
            => this.carAdRepository = carAdRepository;

        public async Task<SearchCarAdsOutputModel> Handle(
            SearchCarAdsQuery request,
            CancellationToken cancellationToken)
        {
            var carAdSpecification = this.GetSpecification(request);

            var carAdListings = await this.carAdRepository.GetCarAdListings(
                carAdSpecification,
                cancellationToken);

            var totalCarAds = await this.carAdRepository.Total(cancellationToken);

            return new SearchCarAdsOutputModel(carAdListings, totalCarAds);
        }

        private Specification<CarAd> GetSpecification(
            SearchCarAdsQuery request)
            => new CarAdByManufacturerSpecification(request.Manufacturer)
                .And(new CarAdByCategorySpecification(request.Category))
                .And(new CarAdByPricePerDaySpecification(request.MinPricePerDay, request.MaxPricePerDay));
    }
}