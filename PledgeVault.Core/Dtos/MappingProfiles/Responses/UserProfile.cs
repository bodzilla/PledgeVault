using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class UserProfile : Profile
{
    public UserProfile() => CreateMap<User, UserResponse>()
        .ForMember(x => x.Pledges, options => options.ExplicitExpansion())
        .ForMember(x => x.Comments, options => options.ExplicitExpansion())
        .ForMember(x => x.Resources, options => options.ExplicitExpansion());
}