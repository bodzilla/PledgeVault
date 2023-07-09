using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using System.Linq;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PoliticianProfile : Profile
{
    public PoliticianProfile() => CreateMap<Politician, PoliticianResponse>()
        .ForMember(x => x.Pledges, options =>
        {
            options.ExplicitExpansion();
            options.MapFrom(x => x.Pledges.OrderByDescending(y => y.Id).Take(10));
        })
        .ForMember(x => x.Party, options
            => options.ExplicitExpansion());
}