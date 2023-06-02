using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PoliticianController : ControllerBase
{
    private readonly IPoliticianService _service;

    public PoliticianController(IPoliticianService service) => _service = service;

    [HttpGet]
    public async Task<IEnumerable<PoliticianResponse>> GetAllAsync() => await _service.GetAllAsync();

    [HttpGet("id/{id:int}")]
    public async Task<PoliticianResponse> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

    [HttpGet("party/{id:int}")]
    public async Task<IEnumerable<PoliticianResponse>> GetByPartyIdAsync(int id) => await _service.GetByPartyIdAsync(id);

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<PoliticianResponse>> GetByNameAsync(string name) => await _service.GetByNameAsync(name);

    [HttpPost]
    public async Task<PoliticianResponse> AddAsync(AddPoliticianRequest request) => await _service.AddAsync(request);

    [HttpPut]
    public async Task<PoliticianResponse> UpdateAsync(UpdatePoliticianRequest request) => await _service.UpdateAsync(request);

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _service.SetInactiveAsync(id);
        return NoContent();
    }
}