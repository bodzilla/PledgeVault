using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
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
    private readonly IMapper _mapper;
    private readonly Mock<ICountryEntityValidator> _mockValidator;

    public SetInactiveCommandHandlerTests()
    {
        _mapper = TestHelper.CreateMapper();
        _context = TestHelper.CreateContext();
        _mockValidator = new Mock<ICountryEntityValidator>();
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
        var handler = new SetInactiveCommandHandler(_context, _mapper);
        var command = new SetInactiveCommand<CountryResponse> { Id = 1 };

        // Act.
        await handler.Handle(command, CancellationToken.None);
        var dbCountry = await _context.Countries.SingleAsync(x => x.Id == command.Id);

        // Assert.
        Assert.False(dbCountry.EntityActive);
    }

    [Fact]
    public async Task Handle_ThrowsNotFoundException_WhenCountryNotFound()
    {
        // Arrange.
        var handler = new SetInactiveCommandHandler(_context, _mapper);
        var command = new SetInactiveCommand<CountryResponse> { Id = 999 };

        // Act and Assert.
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsInvalidRequestException_WhenIdIsNotValid()
    {
        // Arrange.
        var handler = new SetInactiveCommandHandler(_context, _mapper);
        var command = new SetInactiveCommand<CountryResponse> { Id = -1 };

        // Act and Assert.
        await Assert.ThrowsAsync<InvalidRequestException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandIsNull()
    {
        // Arrange.
        var handler = new SetInactiveCommandHandler(_context, _mapper);

        // Act and Assert.
        await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(null, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandRequestIsNull()
    {
        // Arrange.
        var handler = new AddCommandHandler(_context, _mockValidator.Object, _mapper);
        var command = new AddCommand<AddCountryRequest, CountryResponse>();

        // Act and Assert.
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
    }
}