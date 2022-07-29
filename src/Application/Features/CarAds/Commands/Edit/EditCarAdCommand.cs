namespace CarRentalSystem.Application.Features.CarAds.Commands.Edit;

using System.Threading;
using System.Threading.Tasks;
using Common;
using Contracts;
using Dealers;
using Domain.Common;
using Domain.Models.CarAds;
using Exceptions;
using MediatR;

public class EditCarAdCommand : CarAdCommand<EditCarAdCommand>, IRequest<Result>
{
    public class EditCarAdCommandHandler : IRequestHandler<EditCarAdCommand, Result>
    {
        private readonly ICurrentUser currentUser;
        private readonly ICarAdRepository carAdRepository;
        private readonly IDealerRepository dealerRepository;

        public EditCarAdCommandHandler(
            ICurrentUser currentUser,
            ICarAdRepository carAdRepository,
            IDealerRepository dealerRepository)
        {
            this.currentUser = currentUser;
            this.carAdRepository = carAdRepository;
            this.dealerRepository = dealerRepository;
        }

        public async Task<Result> Handle(
            EditCarAdCommand request,
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

            var category = await this.carAdRepository.GetCategory(
                request.Category,
                cancellationToken);

            if (category is null)
            {
                throw new NotFoundException(
                    nameof(category),
                    request.Category);
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

            var manufacturer = await this.carAdRepository.GetManufacturer(
                request.Manufacturer,
                cancellationToken);

            carAd = manufacturer is null
                ? carAd.UpdateManufacturer(request.Manufacturer)
                : carAd.UpdateManufacturer(manufacturer);

            carAd
                .UpdateModel(request.Model)
                .UpdateCategory(category)
                .UpdateImageUrl(request.ImageUrl)
                .UpdatePricePerDay(request.PricePerDay)
                .UpdateOptions(
                    request.HasClimateControl,
                    request.NumberOfSeats,
                    Enumeration.FromValue<TransmissionType>(request.TransmissionType));

            await this.carAdRepository.Save(carAd, cancellationToken);

            return Result.Success;
        }
    }
}