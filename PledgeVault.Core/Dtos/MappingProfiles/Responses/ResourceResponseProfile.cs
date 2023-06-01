using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class ResourceResponseProfile : Profile
{
    public ResourceResponseProfile() => CreateMap<ResourceResponse, Resource>().ReverseMap();
}