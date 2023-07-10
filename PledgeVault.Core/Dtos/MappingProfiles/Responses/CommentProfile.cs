using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class CommentProfile : Profile
{
    public CommentProfile() => CreateMap<Comment, CommentResponse>()
        .ForMember(x => x.User, options =>
        {
            options.ExplicitExpansion();
        })
        .ForMember(x => x.Pledge, options =>
        {
            options.ExplicitExpansion();
        });
}