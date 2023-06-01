using System.Collections.Generic;
using PledgeVault.Core.Contracts;

namespace PledgeVault.Core.Dtos.Responses;

public sealed class PositionResponse : IResponse
{
    public PositionResponse() => Politicians = new List<PoliticianResponse>();

    public int Id { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public ICollection<PoliticianResponse> Politicians { get; set; }
}