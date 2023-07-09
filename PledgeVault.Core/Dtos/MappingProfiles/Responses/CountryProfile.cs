using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using System.Linq;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class CountryProfile : Profile
{
    public CountryProfile() => CreateMap<Country, CountryResponse>()
        .ForMember(x => x.Parties, options =>
        {
            options.ExplicitExpansion();
            options.MapFrom(x => x.Parties.OrderByDescending(y => y.EntityModified).ThenByDescending(y => y.EntityCreated).Take(10));
        });
}