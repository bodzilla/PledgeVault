using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Queries;
using PledgeVault.Services.Queries.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[IgnoreAntiforgeryToken]
[Route("api/[controller]")]
[ApiController]
public sealed class ResourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourceController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetAllQuery<ResourceResponse> { PageOptions = pageOptions }, cancellationToken));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByIdQuery<ResourceResponse> { Id = id }, cancellationToken));

    [HttpGet("pledge/{id:int}")]
    public async Task<IActionResult> GetByPledgeIdAsync(int id, [FromQuery] PageOptions pageOptions, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByPledgeIdQuery { Id = id, PageOptions = pageOptions }, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddResourceRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new AddCommand<AddResourceRequest, ResourceResponse> { Request = request }, cancellationToken));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateResourceRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateCommand<UpdateResourceRequest, ResourceResponse> { Request = request }, cancellationToken));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SetInactiveCommand<ResourceResponse> { Id = id }, cancellationToken);
        return NoContent();
    }
}