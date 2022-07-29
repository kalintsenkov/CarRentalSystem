namespace CarRentalSystem.Application.Features.CarAds.Commands.Create;

using Common;
using FluentValidation;

public class CreateCarAdCommandValidator : AbstractValidator<CreateCarAdCommand>
{
    public CreateCarAdCommandValidator()
        => this.Include(new CarAdCommandValidator<CreateCarAdCommand>());
}