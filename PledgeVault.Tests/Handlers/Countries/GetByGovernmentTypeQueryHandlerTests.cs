using AutoMapper;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Enums.Models;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Handlers.Countries;
using PledgeVault.Services.Queries.Countries;
using PledgeVault.Tests.Helpers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Countries;

public sealed class GetByGovernmentTypeQueryHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByGovernmentTypeQueryHandlerTests()
    {
        _context = TestHelper.CreateContext();
        _mapper = TestHelper.CreateMapper();
    }

    public void Dispose()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }

    [Fact]
    public async Task Handle_ReturnsCorrectData_WhenMatchingCountryExists()
    {
        // Arrange.
        var query = new GetByGovernmentTypeQuery
        {
            GovernmentType = GovernmentType.Democracy,
            PageOptions = new PageOptions()
        };

        var expectedCountry = new Country
        {
            Name = "Test Country",
            GovernmentType = GovernmentType.Democracy
        };

        TestHelper.SeedStub(_context, expectedCountry);

        var handler = new GetByPartyIdQueryHandler(_context, _mapper);

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.NotEmpty(response.Data);
        Assert.Equal(query.PageOptions.PageNumber, response.PageNumber);
        Assert.Equal(query.PageOptions.PageSize, response.PageSize);
        Assert.Equal(1, response.TotalItems);
        Assert.Equal(expectedCountry.Name, response.Data.First().Name);
        Assert.Equal(expectedCountry.GovernmentType, response.Data.First().GovernmentType);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyPage_WhenNoCountriesExist()
    {
        // Arrange.
        var query = new GetByGovernmentTypeQuery
        {
            GovernmentType = GovernmentType.Democracy,
            PageOptions = new PageOptions()
        };

        var handler = new GetByPartyIdQueryHandler(_context, _mapper);

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.Empty(response.Data);
        Assert.Equal(query.PageOptions.PageNumber, response.PageNumber);
        Assert.Equal(query.PageOptions.PageSize, response.PageSize);
        Assert.Equal(0, response.TotalItems);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyPage_WhenNoCountriesMatch()
    {
        // Arrange.
        var query = new GetByGovernmentTypeQuery
        {
            GovernmentType = GovernmentType.Democracy,
            PageOptions = new PageOptions()
        };

        TestHelper.SeedStub(_context, new Country
        {
            Name = "Test Country 1",
            GovernmentType = GovernmentType.Monarchy
        }, new Country
        {
            Name = "Test Country 2",
            GovernmentType = GovernmentType.Dictatorship
        });

        var handler = new GetByPartyIdQueryHandler(_context, _mapper);

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.Empty(response.Data);
        Assert.Equal(query.PageOptions.PageNumber, response.PageNumber);
        Assert.Equal(query.PageOptions.PageSize, response.PageSize);
        Assert.Equal(0, response.TotalItems);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenPageOptionsIsNull()
    {
        // Arrange.
        var query = new GetByGovernmentTypeQuery
        {
            GovernmentType = GovernmentType.Democracy
        };

        var handler = new GetByPartyIdQueryHandler(_context, _mapper);

        // Act and Assert.
        await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(query, CancellationToken.None));
    }
}