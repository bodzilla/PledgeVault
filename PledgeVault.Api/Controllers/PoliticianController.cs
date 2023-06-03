using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Queries;
using PledgeVault.Services.Queries.Politicians;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PoliticianController : ControllerBase
{
    private readonly IMediator _mediator;

    public PoliticianController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetAllQuery<PoliticianResponse> { PageOptions = pageOptions }));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _mediator.Send(new GetByIdQuery<PoliticianResponse> { Id = id }));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByNameQuery<PoliticianResponse> { Name = name, PageOptions = pageOptions }));

    [HttpGet("party/{id:int}")]
    public async Task<IActionResult> GetByPartyIdAsync(int id, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByPartyIdQuery { Id = id, PageOptions = pageOptions }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddPoliticianRequest request)
        => Ok(await _mediator.Send(new AddCommand<AddPoliticianRequest, PoliticianResponse> { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePoliticianRequest request)
        => Ok(await _mediator.Send(new UpdateCommand<UpdatePoliticianRequest, PoliticianResponse> { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetInactiveCommand<PoliticianResponse> { Id = id });
        return NoContent();
    }
}