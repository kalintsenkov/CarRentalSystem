namespace CarRentalSystem.Application.Features.CarAds.Queries.Search
{
    using System.Collections.Generic;

    public class SearchCarAdsOutputModel
    {
        internal SearchCarAdsOutputModel(IEnumerable<CarAdListingModel> carAds, int total)
        {
            this.CarAds = carAds;
            this.Total = total;
        }

        public IEnumerable<CarAdListingModel> CarAds { get; }

        public int Total { get; }
    }
}
