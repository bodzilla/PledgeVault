using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;
using PledgeVault.Core.Security;

namespace PledgeVault.Core.Dtos.MappingProfiles.Requests;

public sealed class AddUserRequestProfile : Profile
{
    public AddUserRequestProfile()
        => CreateMap<AddUserRequest, User>()
            .ForMember(x => x.HashedPassword, x => x.MapFrom(y => AuthenticationManager.HashPassword(y.Password)));
}