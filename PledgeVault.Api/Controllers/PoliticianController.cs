using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Services.Commands.Politicians;
using PledgeVault.Services.Queries.Politicians;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Services.Queries;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PoliticianController : ControllerBase
{
    private readonly IMediator _mediator;

    public PoliticianController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetAllQuery<Politician, PoliticianResponse> { PageOptions = pageOptions }));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _mediator.Send(new GetByIdQuery { Id = id }));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByNameQuery { Name = name, PageOptions = pageOptions }));

    [HttpGet("party/{id:int}")]
    public async Task<IActionResult> GetByPartyIdAsync(int id, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByPartyIdQuery { Id = id, PageOptions = pageOptions }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddPoliticianRequest request)
        => Ok(await _mediator.Send(new AddCommand { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePoliticianRequest request)
        => Ok(await _mediator.Send(new UpdateCommand { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetInactiveCommand { Id = id });
        return NoContent();
    }
}