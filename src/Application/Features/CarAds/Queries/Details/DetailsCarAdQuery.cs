namespace CarRentalSystem.Application.Features.CarAds.Queries.Details;

using System.Threading;
using System.Threading.Tasks;
using Dealers;
using Exceptions;
using MediatR;

public class DetailsCarAdQuery : IRequest<DetailsCarAdOutputModel>
{
    public int Id { get; init; }

    public class DetailsCarAdQueryHandler : IRequestHandler<DetailsCarAdQuery, DetailsCarAdOutputModel>
    {
        private readonly ICarAdRepository carAdRepository;
        private readonly IDealerRepository dealerRepository;

        public DetailsCarAdQueryHandler(
            ICarAdRepository carAdRepository,
            IDealerRepository dealerRepository)
        {
            this.carAdRepository = carAdRepository;
            this.dealerRepository = dealerRepository;
        }

        public async Task<DetailsCarAdOutputModel> Handle(
            DetailsCarAdQuery request,
            CancellationToken cancellationToken)
        {
            var carAd = await this.carAdRepository.GetDetails(
                request.Id,
                cancellationToken);

            if (carAd == null)
            {
                throw new NotFoundException(
                    nameof(carAd),
                    request.Id);
            }

            carAd.Dealer = await this.dealerRepository.GetDetailsByCarId(
                request.Id,
                cancellationToken);

            return carAd;
        }
    }
}