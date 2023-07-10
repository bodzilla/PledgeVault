using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;
using PledgeVault.Core.Security;

namespace PledgeVault.Core.Dtos.MappingProfiles.Requests;

public sealed class UpdateUserPasswordRequestProfile : Profile
{
    public UpdateUserPasswordRequestProfile()
        => CreateMap<UpdateUserPasswordRequest, User>()
            .ForMember(x => x.HashedPassword, x => x.MapFrom(y => AuthenticationManager.HashPassword(y.NewPassword)));
}