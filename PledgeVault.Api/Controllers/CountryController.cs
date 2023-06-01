using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ILogger<CountryController> _logger;
    private readonly ICountryService _service;

    public CountryController(ILogger<CountryController> logger, ICountryService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<CountryResponse>> GetAllAsync()
    {
        try
        {
            return await _service.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting all {nameof(Country)}");
            throw;
        }
    }

    [HttpGet("id/{id:int}")]
    public async Task<CountryResponse> GetByIdAsync(int id)
    {
        try
        {
            return await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Country)} with {nameof(Country.Id)}: '{id}'");
            throw;
        }
    }

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<CountryResponse>> GetByNameAsync(string name)
    {
        try
        {
            return await _service.GetByNameAsync(name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Country)} with {nameof(Country.Name)}: '{name}'");
            throw;
        }
    }

    [HttpPost]
    public async Task<CountryResponse> AddAsync(AddCountryRequest request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(AddCountryRequest)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<CountryResponse> UpdateAsync(UpdateCountryRequest request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(UpdateCountryRequest)}");
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
            _logger.LogError(ex, $"Error setting {nameof(Country)} inactive with {nameof(Country.Id)}: '{id}'");
            throw;
        }
    }
}