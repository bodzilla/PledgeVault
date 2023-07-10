using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record AddUserRequest : IRequest
{
    public string Email { get; init; }

    public string Username { get; init; }

    public string Password { get; init; }
}