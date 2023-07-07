using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Handlers.Parties;
using PledgeVault.Services.Validators;
using PledgeVault.Tests.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Parties;

public sealed class AddCommandHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly AddCommandHandler _handler;
    private readonly Country _parentCountry;

    public AddCommandHandlerTests()
    {
        _context = TestHelper.CreateContext();
        _handler = new AddCommandHandler(_context, new PartyEntityValidator(_context), TestHelper.CreateMapper());
        _parentCountry = TestHelper.SeedStubCountries(_context, 1)[0];
    }

    public void Dispose()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }

    [Fact]
    public async Task Handle_AddsPartySuccessfully_WhenCommandIsValid()
    {
        // Arrange.
        var command = new AddCommand<AddPartyRequest, PartyResponse> { Request = new AddPartyRequest { Name = "Test Party", CountryId = _parentCountry.Id } };

        // Act.
        var response = await _handler.Handle(command, CancellationToken.None);
        var dbParty = await _context.Parties.SingleAsync(x => x.Name == command.Request.Name);

        // Assert.
        Assert.Equal(command.Request.Name, response.Name);
        Assert.Equal(command.Request.CountryId, response.CountryId);

        Assert.Equal(command.Request.Name, dbParty.Name);
        Assert.Equal(command.Request.CountryId, dbParty.CountryId);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    public async Task Handle_ThrowsException_WhenCountryIdInvalid(int countryId)
    {
        // Arrange.
        var command = new AddCommand<AddPartyRequest, PartyResponse> { Request = new AddPartyRequest { Name = "Test Party", CountryId = countryId } };

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidEntityException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenValidationFails()
    {
        // Arrange.
        var command = new AddCommand<AddPartyRequest, PartyResponse> { Request = new AddPartyRequest { Name = null, CountryId = _parentCountry.Id } };

        // Act and Assert.
        await Assert.ThrowsAnyAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandRequestIsNull()
    {
        // Arrange.
        var command = new AddCommand<AddPartyRequest, PartyResponse>();

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandIsNull()
        => await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(null, CancellationToken.None));
}