using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class ResourceProfile : Profile
{
    public ResourceProfile() => CreateMap<Resource, ResourceResponse>().MaxDepth(1);
}