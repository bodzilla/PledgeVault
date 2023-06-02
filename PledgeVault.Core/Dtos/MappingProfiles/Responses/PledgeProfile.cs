using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PledgeProfile : Profile
{
    public PledgeProfile() => CreateMap<Pledge, PledgeResponse>()
        .ForMember(x => x.Resources, options => options.ExplicitExpansion())
        .ForMember(x => x.Politician, options => options.ExplicitExpansion());
}