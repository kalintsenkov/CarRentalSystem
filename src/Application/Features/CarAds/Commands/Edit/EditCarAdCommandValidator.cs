namespace CarRentalSystem.Application.Features.CarAds.Commands.Edit;

using Common;
using FluentValidation;

public class EditCarAdCommandValidator : AbstractValidator<EditCarAdCommand>
{
    public EditCarAdCommandValidator()
        => this.Include(new CarAdCommandValidator<EditCarAdCommand>());
}