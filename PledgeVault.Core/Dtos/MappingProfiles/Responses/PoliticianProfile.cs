using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PoliticianProfile : Profile
{
    public PoliticianProfile() => CreateMap<Politician, PoliticianResponse>().MaxDepth(1);
}