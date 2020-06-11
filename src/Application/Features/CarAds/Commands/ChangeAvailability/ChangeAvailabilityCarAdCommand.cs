namespace CarRentalSystem.Application.Features.CarAds.Commands.ChangeAvailability
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Dealers;
    using MediatR;

    public class ChangeAvailabilityCarAdCommand : IRequest<Result>
    {
        public int Id { get; set; }

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
                var dealerId = await this.dealerRepository.GetDealerId(
                    this.currentUser.UserId,
                    cancellationToken);

                var hasCarAd = await this.dealerRepository.HasCarAd(
                    dealerId,
                    request.Id,
                    cancellationToken);

                if (!hasCarAd)
                {
                    return "You cannot edit this car ad.";
                }

                var carAd = await this.carAdRepository.Find(
                    request.Id, 
                    cancellationToken);

                carAd.ChangeAvailability();

                await this.carAdRepository.Save(carAd, cancellationToken);

                return Result.Success;
            }
        }
    }
}