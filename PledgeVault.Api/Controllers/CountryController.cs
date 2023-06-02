﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Queries;
using PledgeVault.Services.Queries.Countries;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationQuery paginationQuery)
        => Ok(await _mediator.Send(new GetAllQuery<CountryResponse> { PaginationQuery = paginationQuery }));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _mediator.Send(new GetByIdQuery<CountryResponse> { Id = id }));

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name, [FromQuery] PaginationQuery paginationQuery)
        => Ok(await _mediator.Send(new GetByNameQuery<CountryResponse> { Name = name, PaginationQuery = paginationQuery }));

    [HttpGet("government/{type}")]
    public async Task<IActionResult> GetByGovernmentTypeAsync(GovernmentType type, [FromQuery] PaginationQuery paginationQuery)
        => Ok(await _mediator.Send(new GetByGovernmentTypeQuery { GovernmentType = type, PaginationQuery = paginationQuery }));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddCountryRequest request)
        => Ok(await _mediator.Send(new AddCommand<AddCountryRequest, CountryResponse> { Request = request }));

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateCountryRequest request)
        => Ok(await _mediator.Send(new UpdateCommand<PartyResponse> { Request = request }));

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _mediator.Send(new SetInactiveCommand<PartyResponse> { Id = id });
        return NoContent();
    }
}