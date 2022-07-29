namespace CarRentalSystem.Application.Features.CarAds.Queries.Details;

using AutoMapper;
using Dealers.Queries.Common;
using Domain.Common;
using Domain.Models.CarAds;
using Mapping;

public class DetailsCarAdOutputModel : IMapFrom<CarAd>
{
    public int Id { get; private set; }

    public string ManufacturerName { get; private set; } = default!;

    public string Model { get; private set; } = default!;

    public string CategoryName { get; private set; } = default!;

    public string ImageUrl { get; private set; } = default!;

    public decimal PricePerDay { get; private set; }

    public bool HasClimateControl { get; private set; }

    public int NumberOfSeats { get; private set; }

    public string TransmissionType { get; private set; } = default!;

    public DealerOutputModel? Dealer { get; set; } = default!;

    public void Mapping(Profile mapper)
        => mapper
            .CreateMap<CarAd, DetailsCarAdOutputModel>()
            .ForMember(c => c.HasClimateControl, cfg => cfg
                .MapFrom(c => c.Options.HasClimateControl))
            .ForMember(c => c.NumberOfSeats, cfg => cfg
                .MapFrom(c => c.Options.NumberOfSeats))
            .ForMember(c => c.TransmissionType, cfg => cfg
                .MapFrom(c => Enumeration.NameFromValue<TransmissionType>(
                    c.Options.TransmissionType.Value)));
}