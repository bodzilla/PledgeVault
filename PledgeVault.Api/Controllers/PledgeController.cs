using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PledgeController : ControllerBase
{
    private readonly IPledgeService _service;

    public PledgeController(IPledgeService service) => _service = service;

    [HttpGet]
    public async Task<IEnumerable<PledgeResponse>> GetAllAsync() => await _service.GetAllAsync();

    [HttpGet("id/{id:int}")]
    public async Task<PledgeResponse> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

    [HttpGet("politician/{id:int}")]
    public async Task<IEnumerable<PledgeResponse>> GetByPoliticianIdAsync(int id) => await _service.GetByPoliticianIdAsync(id);

    [HttpGet("title/{title}")]
    public async Task<IEnumerable<PledgeResponse>> GetByTitleAsync(string title) => await _service.GetByTitleAsync(title);

    [HttpPost]
    public async Task<PledgeResponse> AddAsync(AddPledgeRequest request) => await _service.AddAsync(request);

    [HttpPut]
    public async Task<PledgeResponse> UpdateAsync(UpdatePledgeRequest request) => await _service.UpdateAsync(request);

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _service.SetInactiveAsync(id);
        return NoContent();
    }
}
