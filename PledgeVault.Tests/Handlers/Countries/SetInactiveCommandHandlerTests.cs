using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Handlers.Countries;
using PledgeVault.Tests.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Countries;

public sealed class SetInactiveCommandHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly SetInactiveCommandHandler _handler;

    public SetInactiveCommandHandlerTests()
    {
        _context = TestHelper.CreateContext();
        _handler = new SetInactiveCommandHandler(_context, TestHelper.CreateMapper());
        TestHelper.SeedStubCountries(_context, 1);
    }

    public void Dispose()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }

    [Fact]
    public async Task Handle_SetsCountryInactive_WhenCommandIsValid()
    {
        // Arrange.
        var command = new SetInactiveCommand<CountryResponse> { Id = 1 };

        // Act.
        await _handler.Handle(command, CancellationToken.None);
        var dbCountry = await _context.Countries.SingleAsync(x => x.Id == command.Id);

        // Assert.
        Assert.False(dbCountry.IsEntityActive);
    }

    [Fact]
    public async Task Handle_ThrowsNotFoundException_WhenCountryNotFound()
    {
        // Arrange.
        var command = new SetInactiveCommand<CountryResponse> { Id = 999 };

        // Act and Assert.
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsInvalidRequestException_WhenIdIsNotValid()
    {
        // Arrange.
        var command = new SetInactiveCommand<CountryResponse> { Id = -1 };

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandRequestIsNull()
    {
        // Arrange.
        var command = new SetInactiveCommand<CountryResponse>();

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandIsNull()
        => await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(null, CancellationToken.None));
}