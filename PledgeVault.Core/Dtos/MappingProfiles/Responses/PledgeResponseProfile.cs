using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class PledgeResponseProfile : Profile
{
    public PledgeResponseProfile() => CreateMap<PledgeResponse, Pledge>().ReverseMap().MaxDepth(1);
}