using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles;

public sealed class UpdateCountryRequestProfile : Profile
{
    public UpdateCountryRequestProfile() => CreateMap<UpdateCountryRequest, Country>();
}