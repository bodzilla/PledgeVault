using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Services.Commands.Pledges;
using PledgeVault.Services.Queries.Pledges;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Queries;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PledgeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PledgeController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetAllQuery<PledgeResponse> { PageOptions = pageOptions }));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _mediator.Send(new GetByIdQuery<PledgeResponse> { Id = id }));

    [HttpGet("politician/{id:int}")]
    public async Task<IActionResult> GetByPoliticianIdAsync(int id, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByPoliticianIdQuery { Id = id, PageOptions = pageOptions }));

    [HttpGet("title/{title}")]
    public async Task<IActionResult> GetByTitleAsync(string title, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByTitleQuery { Title = title, PageOptions = pageOptions }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddPledgeRequest request)
        => Ok(await _mediator.Send(new AddCommand { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePledgeRequest request)
        => Ok(await _mediator.Send(new UpdateCommand { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetInactiveCommand { Id = id });
        return NoContent();
    }
}