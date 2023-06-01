using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles;

public sealed class AddPartyRequestProfile : Profile
{
    public AddPartyRequestProfile() => CreateMap<AddPartyRequest, Party>();
}