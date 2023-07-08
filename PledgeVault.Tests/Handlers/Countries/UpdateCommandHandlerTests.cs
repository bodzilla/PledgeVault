using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Handlers.Countries;
using PledgeVault.Services.Validators;
using PledgeVault.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Countries;

public sealed class UpdateCommandHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly UpdateCommandHandler _handler;
    private readonly IList<Country> _countries;

    public UpdateCommandHandlerTests()
    {
        _context = TestHelper.CreateContext();
        _handler = new UpdateCommandHandler(_context, new CountryEntityValidator(_context), TestHelper.CreateMapper());
        _countries = TestHelper.SeedStubCountries(_context, 2);
    }

    public void Dispose()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }

    [Fact]
    public async Task Handle_UpdatesCountrySuccessfully_WhenCommandIsValid()
    {
        // Arrange.
        var command = new UpdateCommand<UpdateCountryRequest, CountryResponse>
        {
            Request = new UpdateCountryRequest { Id = _countries[0].Id, Name = "Updated Country" }
        };

        // Act.
        var response = await _handler.Handle(command, CancellationToken.None);
        var dbCountry = await _context.Countries.SingleAsync(x => x.Id == command.Request.Id);

        // Assert.
        Assert.Equal(command.Request.Name, response.Name);
        Assert.Equal(command.Request.Name, dbCountry.Name);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenValidationFails()
    {
        // Arrange.
        var nameThatExists = _countries[1].Name;
        var command = new UpdateCommand<UpdateCountryRequest, CountryResponse>
        {
            Request = new UpdateCountryRequest { Id = _countries[0].Id, Name = nameThatExists }
        };

        // Act.
        await Assert.ThrowsAsync<EntityValidationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandRequestIsNull()
    {
        // Arrange.
        var command = new UpdateCommand<UpdateCountryRequest, CountryResponse>();

        // Act and Assert.
        await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandIsNull()
        => await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(null, CancellationToken.None));
}
