using Newtonsoft.Json;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record UpdateUserEmailRequest : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Email { get; init; }
}