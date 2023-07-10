using MediatR;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Commands.Users;
using PledgeVault.Services.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[IgnoreAntiforgeryToken]
[Route("api/[controller]")]
[ApiController]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByIdQuery<UserResponse> { Id = id }, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddUserRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new AddCommand<AddUserRequest, UserResponse> { Request = request }, cancellationToken));

    [HttpPatch("email")]
    public async Task<IActionResult> UpdateEmailAsync(UpdateUserEmailRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateUserEmailCommand { Request = request }, cancellationToken));

    [HttpPatch("username")]
    public async Task<IActionResult> UpdateUsernameAsync(UpdateUserUsernameRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateUserUsernameCommand { Request = request }, cancellationToken));

    [HttpPatch("password")]
    public async Task<IActionResult> UpdatePasswordAsync(UpdateUserPasswordRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateUserPasswordCommand { Request = request }, cancellationToken));
}