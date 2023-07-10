using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using System.Linq;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PledgeProfile : Profile
{
    public PledgeProfile() => CreateMap<Pledge, PledgeResponse>()
        .ForMember(x => x.User, options
            => options.ExplicitExpansion())
        .ForMember(x => x.Politician, options
            => options.ExplicitExpansion())
        .ForMember(x => x.Comments, options =>
        {
            options.ExplicitExpansion();
            options.MapFrom(x => x.Comments.OrderByDescending(y => y.Id).Take(50));
        })
        .ForMember(x => x.Resources, options
            => options.ExplicitExpansion());
}