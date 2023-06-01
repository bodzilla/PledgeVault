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
public class ResourceController : ControllerBase
{
    private readonly ILogger<ResourceController> _logger;
    private readonly IResourceService _service;

    public ResourceController(ILogger<ResourceController> logger, IResourceService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<ResourceResponse>> GetAllAsync()
    {
        try
        {
            return await _service.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting all {nameof(Resource)}");
            throw;
        }
    }

    [HttpGet("id/{id:int}")]
    public async Task<ResourceResponse> GetByIdAsync(int id)
    {
        try
        {
            return await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Resource)} with {nameof(Resource.Id)}: '{id}'");
            throw;
        }
    }

    [HttpGet("pledge/{id:int}")]
    public async Task<IEnumerable<ResourceResponse>> GetByPledgeIdAsync(int id)
    {
        try
        {
            return await _service.GetByPledgeIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting {nameof(Resource)} with {nameof(Resource.PledgeId)}: '{id}'");
            throw;
        }
    }

    [HttpPost]
    public async Task<ResourceResponse> AddAsync(AddResourceRequest request)
    {
        try
        {
            return await _service.AddAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error adding {nameof(AddResourceRequest)}");
            throw;
        }
    }

    [HttpPut]
    public async Task<ResourceResponse> UpdateAsync(UpdateResourceRequest request)
    {
        try
        {
            return await _service.UpdateAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating {nameof(UpdateResourceRequest)}");
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
            _logger.LogError(ex, $"Error setting {nameof(Resource)} inactive with {nameof(Resource.Id)}: '{id}'");
            throw;
        }
    }
}