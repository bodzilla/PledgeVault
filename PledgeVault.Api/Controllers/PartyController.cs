using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Queries;
using PledgeVault.Services.Queries.Parties;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[IgnoreAntiforgeryToken]
[Route("api/[controller]")]
[ApiController]
public sealed class PartyController : ControllerBase
{
    private readonly IMediator _mediator;

    public PartyController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetAllQuery<PartyResponse> { PageOptions = pageOptions }, cancellationToken));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByIdQuery<PartyResponse> { Id = id }, cancellationToken));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, [FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByNameQuery<PartyResponse> { Name = name, PageOptions = pageOptions }, cancellationToken));

    [HttpGet("country/{id:int}")]
    public async Task<IActionResult> GetByCountryIdAsync(int id, [FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByPledgeIdQuery { Id = id, PageOptions = pageOptions }, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddPartyRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new AddCommand<AddPartyRequest, PartyResponse> { Request = request }, cancellationToken));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePartyRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateCommand<UpdatePartyRequest, PartyResponse> { Request = request }, cancellationToken));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SetInactiveCommand<PartyResponse> { Id = id }, cancellationToken);
        return NoContent();
    }
}