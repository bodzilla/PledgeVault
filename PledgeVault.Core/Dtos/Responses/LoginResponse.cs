namespace PledgeVault.Core.Dtos.Responses;

public sealed record LoginResponse
{
    /// <summary>
    /// The generated JWT token.
    /// </summary>
    public string Token { get; init; }
}