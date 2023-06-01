using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PoliticianResponseProfile : Profile
{
    public PoliticianResponseProfile() => CreateMap<PoliticianResponse, Politician>().ReverseMap();
}