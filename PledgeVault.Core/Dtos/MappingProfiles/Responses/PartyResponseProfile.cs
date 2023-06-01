using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PartyResponseProfile : Profile
{
    public PartyResponseProfile() => CreateMap<PartyResponse, Party>().ReverseMap().MaxDepth(1);
}