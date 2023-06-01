using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Requests;

public sealed class UpdateResourceRequestProfile : Profile
{
    public UpdateResourceRequestProfile() => CreateMap<UpdateResourceRequest, Resource>();
}