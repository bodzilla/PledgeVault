using AutoMapper;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Handlers.Countries;
using PledgeVault.Services.Queries;
using PledgeVault.Tests.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Countries;

public sealed class GetByIdQueryHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByIdQueryHandlerTests()
    {
        _mapper = TestHelper.CreateMapper();
        _context = TestHelper.CreateContext();
    }

    public void Dispose()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }

    [Fact]
    public async Task Handle_ReturnsCountry_WhenCountryIdExists()
    {
        // Arrange.
        var query = new GetByIdQuery<CountryResponse> { Id = 1 };

        var expectedCountry = new Country { Name = "Test Country 1" };
        var notExpectedCountry = new Country { Name = "Test Country 2" };
        TestHelper.SeedStub(_context, expectedCountry, notExpectedCountry);

        var handler = new GetByCountryIdQueryHandler(_context, _mapper);

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.NotNull(response);
        Assert.IsType<CountryResponse>(response);
        Assert.Equal(expectedCountry.Id, response.Id);
        Assert.Equal(expectedCountry.Name, response.Name);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenCountryIdDoesNotExist()
    {
        // Arrange.
        var query = new GetByIdQuery<CountryResponse> { Id = 3 };

        TestHelper.SeedStub(_context, new Country { Name = "Test Country 1" }, new Country { Name = "Test Country 2" });

        var handler = new GetByCountryIdQueryHandler(_context, _mapper);

        // Act.
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert.
        Assert.Null(response);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCountryIdIsInvalid()
    {
        // Arrange.
        var query = new GetByIdQuery<CountryResponse> { Id = -1 };
        var handler = new GetByCountryIdQueryHandler(_context, _mapper);

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidRequestException>(() => handler.Handle(query, CancellationToken.None));
    }
}