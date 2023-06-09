﻿using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Handlers.Countries;
using PledgeVault.Services.Validators;
using PledgeVault.Tests.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Countries;

public sealed class AddCommandHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly AddCommandHandler _handler;

    public AddCommandHandlerTests()
    {
        _context = TestHelper.CreateContext();
        _handler = new AddCommandHandler(_context, new CountryEntityValidator(_context), TestHelper.CreateMapper());
    }

    public void Dispose()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }

    [Fact]
    public async Task Handle_AddsCountrySuccessfully_WhenCommandIsValid()
    {
        // Arrange.
        var command = new AddCommand<AddCountryRequest, CountryResponse> { Request = new AddCountryRequest { Name = "Test Country" } };

        // Act.
        var response = await _handler.Handle(command, CancellationToken.None);
        var dbCountry = await _context.Countries.SingleAsync(x => x.Name == command.Request.Name);

        // Assert.
        Assert.Equal(command.Request.Name, response.Name);
        Assert.Equal(command.Request.Name, dbCountry.Name);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenValidationFails()
    {
        // Arrange.
        var command = new AddCommand<AddCountryRequest, CountryResponse> { Request = new AddCountryRequest { Name = null } };

        // Act and Assert.
        await Assert.ThrowsAnyAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandRequestIsNull()
    {
        // Arrange.
        var command = new AddCommand<AddCountryRequest, CountryResponse>();

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandIsNull()
        => await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(null, CancellationToken.None));
}