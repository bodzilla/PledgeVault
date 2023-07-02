using AutoMapper;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
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

public sealed class GetAllQueryHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetAllQueryHandlerTests()
    {
        _mapper = TestHelper.CreateMapper();
        _context = TestHelper.CreateContext();
        TestHelper.SeedStubCountries(_context, 10);
    }

    public void Dispose()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }

    [Fact]
    public async Task Handle_CorrectlyHandlesPagination()
    {
        // Arrange.
        var handler = new GetAllQueryHandler(_context, _mapper);

        var query = new GetAllQuery<CountryResponse>
        {
            PageOptions = new PageOptions
            {
                PageNumber = 3,
                PageSize = 1
            }
        };

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.Equal(query.PageOptions.PageSize, response.Data.Count);
    }

    [Fact]
    public async Task Handle_ReturnsCorrectData_WhenCountriesExist()
    {
        // Arrange.
        var handler = new GetAllQueryHandler(_context, _mapper);
        var query = new GetAllQuery<CountryResponse> { PageOptions = new PageOptions() };

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        var activeCountriesCount = _context.Countries.Count(x => x.EntityActive);
        Assert.Equal(activeCountriesCount, response.TotalItems);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyPage_WhenNoCountriesExist()
    {
        // Arrange.
        var handler = new GetAllQueryHandler(_context, _mapper);
        var query = new GetAllQuery<CountryResponse> { PageOptions = new PageOptions() };

        _context.Countries.RemoveRange(_context.Countries);
        await _context.SaveChangesAsync();

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.Empty(response.Data);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyPage_WhenNoActiveCountriesExist()
    {
        // Arrange.
        var handler = new GetAllQueryHandler(_context, _mapper);
        var query = new GetAllQuery<CountryResponse> { PageOptions = new PageOptions() };

        foreach (var country in _context.Countries) country.EntityActive = false;
        await _context.SaveChangesAsync();

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.Empty(response.Data);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenPageOptionsIsNull()
    {
        // Arrange.
        var handler = new GetAllQueryHandler(_context, _mapper);
        var query = new GetAllQuery<CountryResponse>();

        // Act and Assert.
        await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(query, CancellationToken.None));
    }
}