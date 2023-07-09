using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;
using PledgeVault.Core.Security;

namespace PledgeVault.Core.Dtos.MappingProfiles.Requests;

public sealed class UpdateUserRequestProfile : Profile
{
    public UpdateUserRequestProfile()
        => CreateMap<UpdateUserRequest, User>()
            .ForMember(x => x.HashedPassword, x => x.MapFrom(y => AuthenticationManager.HashPassword(y.Password)));
}