﻿using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PositionProfile : Profile
{
    public PositionProfile() => CreateMap<Position, PositionResponse>()
        .ForMember(x => x.Politicians, options => options.ExplicitExpansion());
}