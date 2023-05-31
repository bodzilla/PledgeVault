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
public class PledgeController : ControllerBase
{
    private readonly ILogger<PledgeController> _logger;
    private readonly IPledgeService _service;

    public PledgeController(ILogger<PledgeController> logger, IPledgeService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Pledge>> GetAllAsync()
    {
        try
        {
            return await _service.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting all {nameof(Pledge)}");
            throw;
        }
    }

    [HttpGet("id/{id:int}")]
    public async Task<Pledge> GetByIdAsync(int id)
    {
        try
        {
            return await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Pledge)} with {nameof(Pledge.Id)}: '{id}'");
            throw;
        }
    }

    [HttpGet("politician/{id}")]
    public async Task<IEnumerable<Pledge>> GetByPoliticianIdAsync(int id)
    {
        try
        {
            return await _service.GetByPoliticianIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Pledge)} with {nameof(Pledge.PoliticianId)}: '{id}'");
            throw;
        }
    }

    [HttpGet("title/{title}")]
    public async Task<IEnumerable<Pledge>> GetByTitleAsync(string title)
    {
        try
        {
            return await _service.GetByTitleAsync(title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Pledge)} with {nameof(Pledge.Title)}: '{title}'");
            throw;
        }
    }

    [HttpPost]
    public async Task<Pledge> AddAsync(Pledge request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(Pledge)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<Pledge> UpdateAsync(Pledge request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(Pledge)}");
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
            _logger.LogError(ex, $"Error setting {nameof(Pledge)} inactive with {nameof(Pledge.Id)}: '{id}'");
            throw;
        }
    }
}