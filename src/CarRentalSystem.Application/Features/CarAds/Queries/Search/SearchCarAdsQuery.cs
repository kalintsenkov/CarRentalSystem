#nullable enable
namespace CarRentalSystem.Application.Features.CarAds.Queries.Search
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class SearchCarAdsQuery : IRequest<SearchCarAdsOutputModel>
    {
        public string? Manufacturer { get; set; }

        public class SearchCarAdsQueryHandler : IRequestHandler<SearchCarAdsQuery, SearchCarAdsOutputModel>
        {
            private readonly ICarAdRepository carAdRepository;

            public SearchCarAdsQueryHandler(ICarAdRepository carAdRepository) 
                => this.carAdRepository = carAdRepository;

            public async Task<SearchCarAdsOutputModel> Handle(
                SearchCarAdsQuery request, 
                CancellationToken cancellationToken)
            {
                var carAdListings = await this.carAdRepository.GetCarAdListings(request.Manufacturer, cancellationToken);
                var totalCarAds = await this.carAdRepository.Total(cancellationToken);

                return new SearchCarAdsOutputModel(carAdListings, totalCarAds);
            }
        }
    }
}
