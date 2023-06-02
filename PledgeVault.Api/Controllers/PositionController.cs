using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PositionController : ControllerBase
{
    private readonly IPositionService _service;

    public PositionController(IPositionService service) => _service = service;

    [HttpGet]
    public async Task<IEnumerable<PositionResponse>> GetAllAsync() => await _service.GetAllAsync();

    [HttpGet("id/{id:int}")]
    public async Task<PositionResponse> GetByIdAsync(int id) => await _service.GetByIdAsync(id);

    [HttpGet("title/{title}")]
    public async Task<IEnumerable<PositionResponse>> GetByTitleAsync(string title) => await _service.GetByTitleAsync(title);

    [HttpPost]
    public async Task<PositionResponse> AddAsync(AddPositionRequest request) => await _service.AddAsync(request);

    [HttpPut]
    public async Task<PositionResponse> UpdateAsync(UpdatePositionRequest request) => await _service.UpdateAsync(request);

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> SetInactiveAsync(int id)
    {
        await _service.SetInactiveAsync(id);
        return NoContent();
    }
}