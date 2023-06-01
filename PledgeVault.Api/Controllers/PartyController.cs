using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartyController : ControllerBase
{
    private readonly ILogger<PartyController> _logger;
    private readonly IPartyService _service;

    public PartyController(ILogger<PartyController> logger, IPartyService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<PartyResponse>> GetAllAsync()
    {
        try
        {
            return await _service.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting all {nameof(Party)}");
            throw;
        }
    }

    [HttpGet("id/{id:int}")]
    public async Task<PartyResponse> GetByIdAsync(int id)
    {
        try
        {
            return await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Party)} with {nameof(Party.Id)}: '{id}'");
            throw;
        }
    }


    [HttpGet("country/{id:int}")]
    public async Task<IEnumerable<PartyResponse>> GetByCountryIdAsync(int id)
    {
        try
        {
            return await _service.GetByCountryIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Party)} with {nameof(Party.CountryId)}: '{id}'");
            throw;
        }
    }

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<PartyResponse>> GetByNameAsync(string name)
    {
        try
        {
            return await _service.GetByNameAsync(name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Party)} with {nameof(Party.Name)}: '{name}'");
            throw;
        }
    }

    [HttpPost]
    public async Task<PartyResponse> AddAsync(AddPartyRequest request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(AddPartyRequest)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<PartyResponse> UpdateAsync(UpdatePartyRequest request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(UpdatePartyRequest)}");
            throw;
        }
    }

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        try
        {
            await _service.SetInactiveAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error setting {nameof(Party)} inactive with {nameof(Party.Id)}: '{id}'");
            throw;
        }
    }
}