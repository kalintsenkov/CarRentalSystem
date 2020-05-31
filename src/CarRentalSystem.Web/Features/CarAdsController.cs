namespace CarRentalSystem.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.CarAds.Queries.Search;
    using Microsoft.AspNetCore.Mvc;

    public class CarAdsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Get([FromQuery] SearchCarAdsQuery query)
            => await this.Send(query);
    }
}
