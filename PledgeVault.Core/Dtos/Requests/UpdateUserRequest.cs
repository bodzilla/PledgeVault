using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class UpdateUserRequest : IRequest
{
    public int Id { get; init; }

    public string Email { get; init; }

    public string Username { get; init; }

    public string Password { get; init; }
}