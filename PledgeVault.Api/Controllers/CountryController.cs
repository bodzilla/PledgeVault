using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryService _service;

    public CountryController(ICountryService service) => _service = service;

    [HttpGet]
    public async Task<IEnumerable<CountryResponse>> GetAllAsync() => await _service.GetAllAsync();

    [HttpGet("id/{id:int}")]
    public async Task<CountryResponse> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<CountryResponse>> GetByNameAsync(string name) => await _service.GetByNameAsync(name);

    [HttpGet("government/{type}")]
    public async Task<IEnumerable<CountryResponse>> GetByGovernmentTypeAsync(GovernmentType type) => await _service.GetByGovernmentTypeAsync(type);

    [HttpPost]
    public async Task<CountryResponse> AddAsync(AddCountryRequest request) => await _service.AddAsync(request);

    [HttpPut]
    public async Task<CountryResponse> UpdateAsync(UpdateCountryRequest request) => await _service.UpdateAsync(request);

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _service.SetInactiveAsync(id);
        return NoContent();
    }
}