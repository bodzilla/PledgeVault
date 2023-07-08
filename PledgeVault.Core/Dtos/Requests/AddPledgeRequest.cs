using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums.Models;
using System;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record AddPledgeRequest : IRequest
{
    public string Title { get; init; }

    public DateTime DatePledged { get; init; }

    public DateTime? DateFulfilled { get; init; }

    public PledgeCategoryType PledgeCategoryType { get; init; }

    public PledgeStatusType PledgeStatusType { get; init; }

    public int UserId { get; init; }

    public int PoliticianId { get; init; }

    public int Score { get; init; }

    public string Summary { get; init; }

    public string FulfilledSummary { get; init; }
}