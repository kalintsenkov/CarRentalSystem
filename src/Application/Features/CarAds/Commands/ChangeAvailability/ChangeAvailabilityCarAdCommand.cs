namespace CarRentalSystem.Application.Features.CarAds.Commands.ChangeAvailability;

using System.Threading;
using System.Threading.Tasks;
using Common;
using Contracts;
using Dealers;
using Exceptions;
using MediatR;

public class ChangeAvailabilityCarAdCommand : EntityCommand<int>, IRequest<Result>
{
    public class ChangeAvailabilityCarAdCommandHandler : IRequestHandler<ChangeAvailabilityCarAdCommand, Result>
    {
        private readonly ICurrentUser currentUser;
        private readonly ICarAdRepository carAdRepository;
        private readonly IDealerRepository dealerRepository;

        public ChangeAvailabilityCarAdCommandHandler(
            ICurrentUser currentUser,
            ICarAdRepository carAdRepository,
            IDealerRepository dealerRepository)
        {
            this.currentUser = currentUser;
            this.carAdRepository = carAdRepository;
            this.dealerRepository = dealerRepository;
        }

        public async Task<Result> Handle(
            ChangeAvailabilityCarAdCommand request,
            CancellationToken cancellationToken)
        {
            var dealerHasCar = await this.currentUser.DealerHasCarAd(
                this.dealerRepository,
                request.Id,
                cancellationToken);

            if (!dealerHasCar)
            {
                return dealerHasCar;
            }

            var carAd = await this.carAdRepository.Find(
                request.Id,
                cancellationToken);

            if (carAd is null)
            {
                throw new NotFoundException(
                    nameof(carAd),
                    request.Id);
            }

            carAd.ChangeAvailability();

            await this.carAdRepository.Save(carAd, cancellationToken);

            return Result.Success;
        }
    }
}