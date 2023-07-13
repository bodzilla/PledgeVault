using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PledgeVault.Core.Contracts.Web;
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
    private readonly IContext _context;

    public UserController(IMediator mediator, IContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpAsync(AddUserRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new AddCommand<AddUserRequest, UserResponse> { Request = request }, cancellationToken));

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetByIdQuery<UserResponse> { Id = id }, cancellationToken));

    [Authorize]
    [HttpPatch("email")]
    public async Task<IActionResult> UpdateEmailAsync(UpdateUserEmailRequest request, CancellationToken cancellationToken)
    {
        var id = await _context.GetCurrentUserId();
        request.Id = id.GetValueOrDefault();
        return Ok(await _mediator.Send(new UpdateUserEmailCommand { Request = request }, cancellationToken));
    }

    [HttpPatch("username")]
    public async Task<IActionResult> UpdateUsernameAsync(UpdateUserUsernameRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateUserUsernameCommand { Request = request }, cancellationToken));

    [HttpPatch("password")]
    public async Task<IActionResult> UpdatePasswordAsync(UpdateUserPasswordRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new UpdateUserPasswordCommand { Request = request }, cancellationToken));
}