namespace CarRentalSystem.Web.Features
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Features.CarAds.Commands.Create;
    using Application.Features.CarAds.Queries.Categories;
    using Application.Features.CarAds.Queries.Details;
    using Application.Features.CarAds.Queries.Search;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CarAdsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Search(
            [FromQuery] SearchCarAdsQuery query)
            => await this.Send(query);

        [HttpGet(Id)]
        public async Task<ActionResult<DetailsCarAdOutputModel>> Details(
            [FromRoute] DetailsCarAdQuery query)
            => await this.Send(query);

        [HttpGet(nameof(Categories))]
        public async Task<ActionResult<IEnumerable<CategoriesCarAdsOutputModel>>> Categories(
            CategoriesCarAdsQuery query)
            => await this.Send(query);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateCarAdOutputModel>> Create(
            CreateCarAdCommand command)
            => await this.Send(command);
    }
}
