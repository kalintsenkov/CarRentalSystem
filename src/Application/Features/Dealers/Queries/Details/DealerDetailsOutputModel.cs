﻿namespace CarRentalSystem.Application.Features.Dealers.Queries.Details;

using AutoMapper;
using Common;
using Domain.Models.Dealers;

public class DealerDetailsOutputModel : DealerOutputModel
{
    public int TotalCarAds { get; private set; }

    public override void Mapping(Profile mapper)
        => mapper
            .CreateMap<Dealer, DealerDetailsOutputModel>()
            .IncludeBase<Dealer, DealerOutputModel>()
            .ForMember(d => d.TotalCarAds, cfg => cfg
                .MapFrom(d => d.CarAds.Count));
}