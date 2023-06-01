using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Dtos.MappingProfiles.Responses;

public sealed class CountryResponseProfile : Profile
{
    public CountryResponseProfile() => CreateMap<CountryResponse, Country>().ReverseMap();
}