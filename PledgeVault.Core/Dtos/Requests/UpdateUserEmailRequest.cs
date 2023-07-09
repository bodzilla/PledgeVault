using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class UpdateUserEmailRequest : IRequest
{
    public int Id { get; init; }

    public string Email { get; init; }
}