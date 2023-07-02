using AutoMapper;
using Bogus;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using System;
using System.Collections.Generic;

namespace PledgeVault.Tests.Helpers;

internal static class TestHelper
{
    public static IMapper CreateMapper()
        => new MapperConfiguration(x =>
        {
            x.CreateMap<AddCountryRequest, Country>();
            x.CreateMap<Country, CountryResponse>();
            x.CreateMap<Party, PartyResponse>();
            x.CreateMap<Politician, PoliticianResponse>();
            x.CreateMap<Pledge, PledgeResponse>();
            x.CreateMap<Resource, ResourceResponse>();
        }).CreateMapper();

    public static PledgeVaultContext CreateContext()
        => new(new DbContextOptionsBuilder<PledgeVaultContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

    public static void SeedDatabase(PledgeVaultContext context, int size)
    {
        context.Countries.AddRange(GenerateCountries(size));
        context.SaveChanges();
    }

    private static IEnumerable<Country> GenerateCountries(int size)
        => new Faker<Country>().RuleFor(x => x.Name, x => x.Address.Country()).Generate(size);
}