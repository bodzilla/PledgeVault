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
    public async Task<IEnumerable<PledgeResponse>> GetAllAsync()
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
    public async Task<PledgeResponse> GetByIdAsync(int id)
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

    [HttpGet("politician/{id:int}")]
    public async Task<IEnumerable<PledgeResponse>> GetByPoliticianIdAsync(int id)
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
    public async Task<IEnumerable<PledgeResponse>> GetByTitleAsync(string title)
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
    public async Task<PledgeResponse> AddAsync(AddPledgeRequest request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(AddPledgeRequest)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<PledgeResponse> UpdateAsync(UpdatePledgeRequest request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(UpdatePledgeRequest)}");
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