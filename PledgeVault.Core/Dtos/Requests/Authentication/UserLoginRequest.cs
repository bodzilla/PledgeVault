using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests.Authentication;

public sealed record UserLoginRequest : IRequest
{
    public string Email { get; init; }

    public string Password { get; init; }
}