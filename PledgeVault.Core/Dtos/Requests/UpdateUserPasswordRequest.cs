using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record UpdateUserPasswordRequest : IRequest
{
    public int Id { get; init; }

    /// <summary>
    /// The current raw password.
    /// </summary>
    public string CurrentPassword { get; init; }

    /// <summary>
    /// The new raw password.
    /// </summary>
    public string NewPassword { get; init; }
}