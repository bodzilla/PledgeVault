using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PledgeVault.Core.Contracts;
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
    public async Task<IEnumerable<Country>> GetAllAsync()
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

    [HttpGet("{id:int}")]
    public async Task<Country> GetAsync(int id)
    {
        try
        {
            return await _service.GetAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Country)} with {nameof(Country.Id)}: '{id}'");
            throw;
        }
    }

    [HttpPost]
    public async Task<Country> AddAsync(Country request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(Country)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<Country> UpdateAsync(Country request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(Country)}");
            throw;
        }
    }

    [HttpPatch("{id:int}")]
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