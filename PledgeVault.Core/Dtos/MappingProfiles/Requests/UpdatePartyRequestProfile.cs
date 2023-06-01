using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Requests;

public sealed class UpdatePartyRequestProfile : Profile
{
    public UpdatePartyRequestProfile() => CreateMap<UpdatePartyRequest, Party>();
}