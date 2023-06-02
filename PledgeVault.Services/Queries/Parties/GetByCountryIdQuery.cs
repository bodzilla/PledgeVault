﻿using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Parties;

public sealed class GetByCountryIdQuery : IRequest<PageResponse<PartyResponse>>
{
    public int Id { get; set; }

    public Page Page { get; set; }
}