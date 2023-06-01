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
public class PoliticianController : ControllerBase
{
    private readonly ILogger<PoliticianController> _logger;
    private readonly IPoliticianService _service;

    public PoliticianController(ILogger<PoliticianController> logger, IPoliticianService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<PoliticianResponse>> GetAllAsync()
    {
        try
        {
            return await _service.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting all {nameof(Politician)}");
            throw;
        }
    }

    [HttpGet("id/{id:int}")]
    public async Task<PoliticianResponse> GetByIdAsync(int id)
    {
        try
        {
            return await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Politician)} with {nameof(Politician.Id)}: '{id}'");
            throw;
        }
    }


    [HttpGet("party/{id:int}")]
    public async Task<IEnumerable<PoliticianResponse>> GetByPartyIdAsync(int id)
    {
        try
        {
            return await _service.GetByPartyIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Politician)} with {nameof(Politician.PartyId)}: '{id}'");
            throw;
        }
    }

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<PoliticianResponse>> GetByNameAsync(string name)
    {
        try
        {
            return await _service.GetByNameAsync(name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Politician)} with {nameof(Politician.Name)}: '{name}'");
            throw;
        }
    }

    [HttpPost]
    public async Task<PoliticianResponse> AddAsync(AddPoliticianRequest request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(AddPoliticianRequest)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<PoliticianResponse> UpdateAsync(UpdatePoliticianRequest request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(UpdatePoliticianRequest)}");
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
            _logger.LogError(ex, $"Error setting {nameof(Politician)} inactive with {nameof(Politician.Id)}: '{id}'");
            throw;
        }
    }
}