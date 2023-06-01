using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartyController : ControllerBase
{
    private readonly IPartyService _service;

    public PartyController(IPartyService service) => _service = service;

    [HttpGet]
    public async Task<IEnumerable<PartyResponse>> GetAllAsync() => await _service.GetAllAsync();

    [HttpGet("id/{id:int}")]
    public async Task<PartyResponse> GetByIdAsync(int id) => await _service.GetByIdAsync(id);


    [HttpGet("country/{id:int}")]
    public async Task<IEnumerable<PartyResponse>> GetByCountryIdAsync(int id) => await _service.GetByCountryIdAsync(id);

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<PartyResponse>> GetByNameAsync(string name) => await _service.GetByNameAsync(name);

    [HttpPost]
    public async Task<PartyResponse> AddAsync(AddPartyRequest request) => await _service.AddAsync(request);

    [HttpPut]
    public async Task<PartyResponse> UpdateAsync(UpdatePartyRequest request) => await _service.UpdateAsync(request);

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _service.SetInactiveAsync(id);
        return NoContent();
    }
}