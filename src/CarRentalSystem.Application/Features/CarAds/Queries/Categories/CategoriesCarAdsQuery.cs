namespace CarRentalSystem.Application.Features.CarAds.Queries.Categories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class CategoriesCarAdsQuery : IRequest<IEnumerable<CategoriesCarAdsOutputModel>>
    {
        public class CategoriesCarAdsQueryHandler : IRequestHandler<CategoriesCarAdsQuery, IEnumerable<CategoriesCarAdsOutputModel>>
        {
            private readonly ICarAdRepository carAdRepository;

            public CategoriesCarAdsQueryHandler(ICarAdRepository carAdRepository)
                => this.carAdRepository = carAdRepository;

            public async Task<IEnumerable<CategoriesCarAdsOutputModel>> Handle(
                CategoriesCarAdsQuery request,
                CancellationToken cancellationToken)
                => await this.carAdRepository.GetCarAdCategories(cancellationToken);
        }
    }
}