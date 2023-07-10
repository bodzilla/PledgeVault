using AutoMapper;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Handlers.Countries;
using PledgeVault.Services.Queries;
using PledgeVault.Tests.Helpers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Countries;

public sealed class GetByNameQueryHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByNameQueryHandlerTests()
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
    public async Task Handle_ReturnsCountries_WhenCountriesAreFound()
    {
        // Arrange.
        var query = new GetByNameQuery<CountryResponse> { Name = "1", PageOptions = new PageOptions() };

        var expectedCountry = new Country { Name = "Test Country 1" };
        var notExpectedCountry = new Country { Name = "Test Country 2" };
        TestHelper.SeedStub(_context, expectedCountry, notExpectedCountry);

        var handler = new GetByNameQueryHandler(_context, _mapper);

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.NotNull(response);
        Assert.IsType<Page<CountryResponse>>(response);
        Assert.Equal(query.PageOptions.PageNumber, response.PageNumber);
        Assert.Equal(query.PageOptions.PageSize, response.PageSize);
        Assert.Equal(1, response.TotalItems);
        Assert.Single(response.Data);
        Assert.Equal(expectedCountry.Name, response.Data.First().Name);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyPage_WhenNoCountriesMatch()
    {
        // Arrange.
        var query = new GetByNameQuery<CountryResponse> { Name = "3", PageOptions = new PageOptions() };

        TestHelper.SeedStub(_context, new Country { Name = "Test Country 1", }, new Country { Name = "Test Country 2", });

        var handler = new GetByNameQueryHandler(_context, _mapper);

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.Empty(response.Data);
        Assert.Equal(query.PageOptions.PageNumber, response.PageNumber);
        Assert.Equal(query.PageOptions.PageSize, response.PageSize);
        Assert.Equal(0, response.TotalItems);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRequestIsNull()
    {
        // Arrange.
        var handler = new GetByNameQueryHandler(_context, _mapper);

        // Act and Assert.
        await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(null, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRequestNameIsNull()
    {
        // Arrange.
        var query = new GetByNameQuery<CountryResponse>();
        var handler = new GetByNameQueryHandler(_context, _mapper);

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidRequestException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRequestNameIsEmpty()
    {
        // Arrange.
        var query = new GetByNameQuery<CountryResponse> { Name = String.Empty };
        var handler = new GetByNameQueryHandler(_context, _mapper);

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidRequestException>(() => handler.Handle(query, CancellationToken.None));
    }
}