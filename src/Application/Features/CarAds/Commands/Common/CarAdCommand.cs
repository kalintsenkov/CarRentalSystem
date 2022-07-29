namespace CarRentalSystem.Application.Features.CarAds.Commands.Common;

public abstract class CarAdCommand<TCommand> : EntityCommand<int>
    where TCommand : EntityCommand<int>
{
    public string Manufacturer { get; init; } = default!;

    public string Model { get; init; } = default!;

    public int Category { get; init; }

    public string ImageUrl { get; init; } = default!;

    public decimal PricePerDay { get; init; }

    public bool HasClimateControl { get; init; }

    public int NumberOfSeats { get; init; }

    public int TransmissionType { get; init; }
}