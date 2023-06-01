using AutoMapper;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Requests;

public sealed class AddPoliticianRequestProfile : Profile
{
    public AddPoliticianRequestProfile() => CreateMap<AddPoliticianRequest, Politician>();
}