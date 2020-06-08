namespace CarRentalSystem.Application.Features.CarAds.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using CarRentalSystem.Application.Contracts;
    using CarRentalSystem.Application.Features.Dealers;
    using MediatR;

    public class DeleteCarAdCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteCarAdCommandHandler : IRequestHandler<DeleteCarAdCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly ICarAdRepository carAdRepository;
            private readonly IDealerRepository dealerRepository;

            public DeleteCarAdCommandHandler(
                ICurrentUser currentUser,
                ICarAdRepository carAdRepository,
                IDealerRepository dealerRepository)
            {
                this.currentUser = currentUser;
                this.carAdRepository = carAdRepository;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(
                DeleteCarAdCommand request, 
                CancellationToken cancellationToken)
            {
                var dealerId = await dealerRepository.GetDealerId(
                currentUser.UserId,
                cancellationToken);

                var dealerHasCar = await dealerRepository.HasCarAd(
                    dealerId,
                    request.Id,
                    cancellationToken);

                if (!dealerHasCar)
                {
                    return "You cannot edit this car ad.";
                }

                return await this.carAdRepository.Delete(request.Id, cancellationToken);
            }
        }
    }
}
