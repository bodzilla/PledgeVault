using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Users;

public sealed record UpdateUserUsernameCommand : IRequest<UserResponse>
{
    public UpdateUserUsernameRequest Request { get; init; }
}