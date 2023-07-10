using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using System.Linq;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PartyProfile : Profile
{
    public PartyProfile() => CreateMap<Party, PartyResponse>()
        .ForMember(x => x.Politicians, options =>
        {
            options.ExplicitExpansion();
            options.MapFrom(x => x.Politicians.OrderByDescending(y => y.EntityModified).ThenByDescending(y => y.EntityCreated).Take(10));
        })
        .ForMember(x => x.Country, options => options.ExplicitExpansion());
}