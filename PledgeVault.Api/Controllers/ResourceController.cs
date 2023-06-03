using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Services.Commands.Resources;
using PledgeVault.Services.Queries.Resources;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Queries;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ResourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourceController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetAllQuery<ResourceResponse> { PageOptions = pageOptions }));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _mediator.Send(new GetByIdQuery<ResourceResponse> { Id = id }));

    [HttpGet("pledge/{id:int}")]
    public async Task<IActionResult> GetByPledgeIdAsync(int id, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByPledgeIdQuery { Id = id, PageOptions = pageOptions }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddResourceRequest request)
        => Ok(await _mediator.Send(new AddCommand { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateResourceRequest request)
        => Ok(await _mediator.Send(new UpdateCommand { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetInactiveCommand { Id = id });
        return NoContent();
    }
}