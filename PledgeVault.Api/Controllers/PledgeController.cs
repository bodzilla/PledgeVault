using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Queries;
using PledgeVault.Services.Queries.Pledges;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PledgeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PledgeController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetAllQuery<PledgeResponse> { PageOptions = pageOptions }, cancellationToken));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByIdQuery<PledgeResponse> { Id = id }, cancellationToken));

    [HttpGet("politician/{id:int}")]
    public async Task<IActionResult> GetByPoliticianIdAsync(int id, [FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByPoliticianIdQuery { Id = id, PageOptions = pageOptions }, cancellationToken));

    [HttpGet("title/{title}")]
    public async Task<IActionResult> GetByTitleAsync(string title, [FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByTitleQuery { Title = title, PageOptions = pageOptions }, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddPledgeRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new AddCommand<AddPledgeRequest, PledgeResponse> { Request = request }, cancellationToken));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePledgeRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateCommand<UpdatePledgeRequest, PledgeResponse> { Request = request }, cancellationToken));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SetInactiveCommand<PledgeResponse> { Id = id }, cancellationToken);
        return NoContent();
    }
}