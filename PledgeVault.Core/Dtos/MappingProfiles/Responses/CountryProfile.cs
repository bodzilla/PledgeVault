using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class CountryProfile : Profile
{
    public CountryProfile() => CreateMap<Country, CountryResponse>().ForMember(x => x.Parties, options => options.ExplicitExpansion());
}