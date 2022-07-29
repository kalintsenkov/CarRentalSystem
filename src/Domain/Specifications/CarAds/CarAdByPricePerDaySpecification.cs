namespace CarRentalSystem.Domain.Specifications.CarAds;

using System;
using System.Linq.Expressions;
using Models.CarAds;

public class CarAdByPricePerDaySpecification : Specification<CarAd>
{
    private readonly decimal minPrice;
    private readonly decimal maxPrice;

    public CarAdByPricePerDaySpecification(decimal? minPrice, decimal? maxPrice)
    {
        this.minPrice = minPrice ?? default;
        this.maxPrice = maxPrice ?? int.MaxValue;
    }

    public override Expression<Func<CarAd, bool>> ToExpression()
        => carAd => this.minPrice < carAd.PricePerDay && carAd.PricePerDay < this.maxPrice;
}