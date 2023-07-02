using AutoMapper;
using Bogus;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities;
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
            x.CreateMap<UpdateCountryRequest, Country>();
            x.CreateMap<Country, CountryResponse>();
            x.CreateMap<Party, PartyResponse>();
            x.CreateMap<Politician, PoliticianResponse>();
            x.CreateMap<Pledge, PledgeResponse>();
            x.CreateMap<Resource, ResourceResponse>();
        }).CreateMapper();

    public static PledgeVaultContext CreateContext()
        => new(new DbContextOptionsBuilder<PledgeVaultContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);


    public static void SeedStub<T>(PledgeVaultContext context, params T[] entities) where T : class, IEntity
    {
        if (entities is not { Length: > 0 }) throw new ArgumentException($"No {nameof(entities)} provided to seed.");
        var dbSet = context.Set<T>();
        foreach (var entity in entities) dbSet.Add(entity);
        context.SaveChanges();
    }

    public static void SeedStubCountries(PledgeVaultContext context, int size)
    {
        if (size <= 0) throw new ArgumentException($"Invalid {nameof(size)} provided to seed.");

        var countries = GenerateCountries(size);
        context.Countries.AddRange(countries);
        context.SaveChanges();

        // Detach entities after saving to avoid conflicts with tests (this potential conflict only exists while testing).
        foreach (var country in countries) context.Entry(country).State = EntityState.Detached;
    }

    private static ICollection<Country> GenerateCountries(int size)
        => new Faker<Country>().RuleFor(x => x.Name, x => x.Address.Country()).Generate(size);
}