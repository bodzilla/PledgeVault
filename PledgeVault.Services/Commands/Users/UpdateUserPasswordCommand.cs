using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Users;

public sealed record UpdateUserPasswordCommand : IRequest<UserResponse>
{
    public UpdateUserPasswordRequest Request { get; init; }
}