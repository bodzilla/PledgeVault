using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums.Models;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Responses;

public sealed record PledgeResponse : IResponse
{
    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; init; }

    public string Title { get; init; }

    public DateTime DatePledged { get; init; }

    public DateTime? DateFulfilled { get; init; }

    public PledgeCategoryType PledgeCategoryType { get; init; }

    public PledgeStatusType PledgeStatusType { get; init; }

    public int UserId { get; init; }

    public UserResponse User { get; init; }

    public int PoliticianId { get; init; }

    public PoliticianResponse Politician { get; init; }

    public int Score { get; init; }

    public string Summary { get; init; }

    public string FulfilledSummary { get; init; }

    public IReadOnlyCollection<CommentResponse> Comments { get; init; }

    public IReadOnlyCollection<ResourceResponse> Resources { get; init; }
}