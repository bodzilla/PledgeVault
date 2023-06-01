﻿using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles;

public sealed class AddCountryRequestProfile : Profile
{
    public AddCountryRequestProfile() => CreateMap<AddCountryRequest, Country>();
}