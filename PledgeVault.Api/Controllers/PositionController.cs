using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase
{
    private readonly ILogger<PositionController> _logger;
    private readonly IPositionService _service;

    public PositionController(ILogger<PositionController> logger, IPositionService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Position>> GetAllAsync()
    {
        try
        {
            return await _service.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting all {nameof(Position)}");
            throw;
        }
    }

    [HttpGet("id/{id:int}")]
    public async Task<Position> GetByIdAsync(int id)
    {
        try
        {
            return await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Position)} with {nameof(Position.Id)}: '{id}'");
            throw;
        }
    }

    [HttpGet("title/{title}")]
    public async Task<IEnumerable<Position>> GetByNameAsync(string title)
    {
        try
        {
            return await _service.GetByTitleAsync(title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Position)} with {nameof(Position.Title)}: '{title}'");
            throw;
        }
    }

    [HttpPost]
    public async Task<Position> AddAsync(AddPositionRequest request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(AddPositionRequest)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<Position> UpdateAsync(UpdatePositionRequest request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(UpdatePositionRequest)}");
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
            _logger.LogError(ex, $"Error setting {nameof(Position)} inactive with {nameof(Position.Id)}: '{id}'");
            throw;
        }
    }
}