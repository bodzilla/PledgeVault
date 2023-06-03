using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Queries;
using PledgeVault.Services.Queries.Countries;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetAllQuery<CountryResponse> { PageOptions = pageOptions }));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _mediator.Send(new GetByIdQuery<CountryResponse> { Id = id }));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByNameQuery<CountryResponse> { Name = name, PageOptions = pageOptions }));

    [HttpGet("government/{type}")]
    public async Task<IActionResult> GetByGovernmentTypeAsync(GovernmentType type, [FromQuery] PageOptions pageOptions)
        => Ok(await _mediator.Send(new GetByGovernmentTypeQuery { GovernmentType = type, PageOptions = pageOptions }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddCountryRequest request)
        => Ok(await _mediator.Send(new AddCommand<AddCountryRequest, CountryResponse> { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCountryRequest request)
        => Ok(await _mediator.Send(new UpdateCommand<UpdateCountryRequest, CountryResponse> { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetInactiveCommand<CountryResponse> { Id = id });
        return NoContent();
    }
}