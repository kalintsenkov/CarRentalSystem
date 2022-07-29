namespace CarRentalSystem.Application.Features.CarAds.Commands.Create;

public class CreateCarAdOutputModel
{
    public CreateCarAdOutputModel(int id) => this.Id = id;

    public int Id { get; }
}