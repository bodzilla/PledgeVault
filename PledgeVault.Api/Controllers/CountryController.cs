using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Services.Commands.Countries;
using PledgeVault.Services.Queries.Countries;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IEnumerable<CountryResponse>> GetAllAsync() => await _mediator.Send(new GetAllCountriesQuery());

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _mediator.Send(new GetCountryByIdQuery { Id = id }));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name) => Ok(await _mediator.Send(new GetCountriesByNameQuery { Name = name }));

    [HttpGet("government/{type}")]
    public async Task<IActionResult> GetByGovernmentTypeAsync(GovernmentType type) => Ok(await _mediator.Send(new GetCountriesByGovernmentTypeQuery { GovernmentType = type }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddCountryRequest request) => Ok(await _mediator.Send(new AddCountryCommand { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCountryRequest request) => Ok(await _mediator.Send(new UpdateCountryCommand { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetCountryInactiveCommand { Id = id });
        return NoContent();
    }
}