using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record UpdateUserEmailRequest : IRequest
{
    public int Id { get; init; }

    public string Email { get; init; }
}