using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Users;

public sealed record UpdateUserEmailCommand : IRequest<UserResponse>
{
    public UpdateUserEmailRequest Request { get; init; }
}