using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using System.Linq;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class UserProfile : Profile
{
    public UserProfile() => CreateMap<User, UserResponse>()
        .ForMember(x => x.Pledges, options =>
        {
            options.ExplicitExpansion();
            options.MapFrom(x => x.Pledges.OrderByDescending(y => y.Id).Take(10));
        })
        .ForMember(x => x.Comments, options =>
        {
            options.ExplicitExpansion();
            options.MapFrom(x => x.Comments.OrderByDescending(y => y.Id).Take(10));
        })
        .ForMember(x => x.Resources, options =>
        {
            options.ExplicitExpansion();
            options.MapFrom(x => x.Resources.OrderByDescending(y => y.Id).Take(10));
        });
}