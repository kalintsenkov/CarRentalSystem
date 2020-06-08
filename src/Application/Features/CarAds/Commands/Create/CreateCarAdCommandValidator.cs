namespace CarRentalSystem.Application.Features.CarAds.Commands.Create
{
    using System;
    using Domain.Common;
    using Domain.Models.CarAds;
    using FluentValidation;

    public class CreateCarAdCommandValidator : AbstractValidator<CreateCarAdCommand>
    {
        public CreateCarAdCommandValidator(ICarAdRepository carAdRepository)
        {
            this.RuleFor(c => c.Category)
                .MustAsync(async (category, token) => await carAdRepository
                    .GetCategory(category, token) != null)
                .WithMessage("'{PropertyName}' does not exist.");

            this.RuleFor(c => c.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                .WithMessage("'{PropertyName}' must be a valid url.")
                .NotEmpty();

            this.RuleFor(c => c.TransmissionType)
                .Must(BeAValidTransmissionType)
                .WithMessage("'{PropertyName}' is not a valid transmission type.");
        }

        private static bool BeAValidTransmissionType(int transmissionType)
        {
            try
            {
                Enumeration.FromValue<TransmissionType>(transmissionType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}