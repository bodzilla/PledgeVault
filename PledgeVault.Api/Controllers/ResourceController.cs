using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ResourceController : ControllerBase
{
    private readonly IResourceService _service;

    public ResourceController(IResourceService service) => _service = service;

    [HttpGet]
    public async Task<IEnumerable<ResourceResponse>> GetAllAsync() => await _service.GetAllAsync();

    [HttpGet("id/{id:int}")]
    public async Task<ResourceResponse> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

    [HttpGet("pledge/{id:int}")]
    public async Task<IEnumerable<ResourceResponse>> GetByPledgeIdAsync(int id) => await _service.GetByPledgeIdAsync(id);

    [HttpPost]
    public async Task<ResourceResponse> AddAsync(AddResourceRequest request) => await _service.AddAsync(request);

    [HttpPut]
    public async Task<ResourceResponse> UpdateAsync(UpdateResourceRequest request) => await _service.UpdateAsync(request);

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _service.SetInactiveAsync(id);
        return NoContent();
    }
}