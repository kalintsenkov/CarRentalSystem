namespace CarRentalSystem.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Dealers.Queries.Details;
    using Microsoft.AspNetCore.Mvc;

    public class DealersController : ApiController
    {
        [HttpGet(Id)]
        public async Task<ActionResult<DealerDetailsOutputModel>> Details(
            [FromRoute] DealerDetailsQuery query)
            => await this.Send(query);
    }
}
