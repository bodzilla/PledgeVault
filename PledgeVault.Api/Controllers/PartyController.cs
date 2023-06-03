using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Services.Commands.Parties;
using PledgeVault.Services.Queries.Parties;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Services.Queries;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PartyController : ControllerBase
{
    private readonly IMediator _mediator;

    public PartyController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetAllQuery<Party, PartyResponse> { PageOptions = pageOptions }));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _mediator.Send(new GetByIdQuery { Id = id }));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByNameQuery { Name = name, PageOptions = pageOptions }));

    [HttpGet("country/{id:int}")]
    public async Task<IActionResult> GetByCountryIdAsync(int id, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByCountryIdQuery { Id = id, PageOptions = pageOptions }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddPartyRequest request)
        => Ok(await _mediator.Send(new AddCommand { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdatePartyRequest request)
        => Ok(await _mediator.Send(new UpdateCommand { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetInactiveCommand { Id = id });
        return NoContent();
    }
}