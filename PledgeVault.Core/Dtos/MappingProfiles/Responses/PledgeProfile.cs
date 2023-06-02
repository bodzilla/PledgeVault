using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PledgeProfile : Profile
{
    public PledgeProfile() => CreateMap<Pledge, PledgeResponse>().MaxDepth(1);
}