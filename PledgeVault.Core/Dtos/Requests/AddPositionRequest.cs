using PledgeVault.Core.Contracts;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class AddPositionRequest : IRequest
{
    public string Title { get; set; }

    public string Summary { get; set; }
}