namespace CarRentalSystem.Web.Features;

using System.Threading.Tasks;
using Application.Features;
using Application.Features.Dealers.Commands.Edit;
using Application.Features.Dealers.Queries.Details;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class DealersController : ApiController
{
    [HttpGet(Id)]
    public async Task<ActionResult<DealerDetailsOutputModel?>> Details(
        [FromRoute] DealerDetailsQuery query)
        => await this.Send(query);

    [Authorize]
    [HttpPut(Id)]
    public async Task<ActionResult> Edit(
        int id, EditDealerCommand command)
        => await this.Send(command.SetId(id));
}