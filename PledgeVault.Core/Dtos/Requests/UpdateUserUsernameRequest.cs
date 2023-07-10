using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class UpdateUserUsernameRequest : IRequest
{
    public int Id { get; init; }

    public string Username { get; init; }
}