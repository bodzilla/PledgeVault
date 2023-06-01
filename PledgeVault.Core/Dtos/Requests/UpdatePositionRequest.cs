using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class UpdatePositionRequest : IRequest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }
}