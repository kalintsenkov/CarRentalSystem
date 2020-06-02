namespace CarRentalSystem.Application.Features.Dealers.Queries.Details
{
    using AutoMapper;
    using Domain.Models.Dealers;
    using Mapping;

    public class DealerDetailsOutputModel : IMapFrom<Dealer>
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string PhoneNumber { get; private set; } = default!;

        public int TotalCarAds { get; private set; }

        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<Dealer, DealerDetailsOutputModel>()
                .ForMember(
                    d => d.PhoneNumber, 
                    cfg => cfg.MapFrom(d => d.PhoneNumber.Number))
                .ForMember(
                    d => d.TotalCarAds, 
                    cfg => cfg.MapFrom(d => d.CarAds.Count));
    }
}
