﻿namespace CarRentalSystem.Application.Features.CarAds;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain.Models.CarAds;
using Domain.Specifications;
using Queries.Categories;
using Queries.Details;
using Queries.Search;

public interface ICarAdRepository : IRepository<CarAd>
{
    Task<bool> Delete(
        int id, 
        CancellationToken cancellationToken = default);

    Task<CarAd?> Find(
        int id,
        CancellationToken cancellationToken = default);

    Task<Category?> GetCategory(
        int categoryId,
        CancellationToken cancellationToken = default);

    Task<Manufacturer?> GetManufacturer(
        string manufacturerName,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
        Specification<CarAd> specification,
        CancellationToken cancellationToken = default);

    Task<DetailsCarAdOutputModel?> GetDetails(
        int id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CategoriesCarAdsOutputModel>> GetCarAdCategories(
        CancellationToken cancellationToken = default);

    Task<int> Total(CancellationToken cancellationToken = default);
}