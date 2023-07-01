using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Queries;
using PledgeVault.Services.Queries.Politicians;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[IgnoreAntiforgeryToken]
[Route("api/[controller]")]
[ApiController]
public sealed class PoliticianController : ControllerBase
{
    private readonly IMediator _mediator;

    public PoliticianController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetAllQuery<PoliticianResponse> { PageOptions = pageOptions }, cancellationToken));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByIdQuery<PoliticianResponse> { Id = id }, cancellationToken));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, [FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByNameQuery<PoliticianResponse> { Name = name, PageOptions = pageOptions }, cancellationToken));

    [HttpGet("party/{id:int}")]
    public async Task<IActionResult> GetByPartyIdAsync(int id, [FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByPartyIdQuery { Id = id, PageOptions = pageOptions }, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddPoliticianRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new AddCommand<AddPoliticianRequest, PoliticianResponse> { Request = request }, cancellationToken));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePoliticianRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateCommand<UpdatePoliticianRequest, PoliticianResponse> { Request = request }, cancellationToken));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SetInactiveCommand<PoliticianResponse> { Id = id }, cancellationToken);
        return NoContent();
    }
}