﻿using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PositionResponseProfile : Profile
{
    public PositionResponseProfile() => CreateMap<PositionResponse, Position>().ReverseMap();
}