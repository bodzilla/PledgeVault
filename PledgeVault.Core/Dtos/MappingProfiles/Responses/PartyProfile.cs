using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PartyProfile : Profile
{
    public PartyProfile() => CreateMap<Party, PartyResponse>().MaxDepth(1);
}