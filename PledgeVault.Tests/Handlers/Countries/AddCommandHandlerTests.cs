using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using PledgeVault.Services.Handlers.Countries;
using PledgeVault.Tests.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PledgeVault.Tests.Handlers.Countries;

public sealed class AddCommandHandlerTests : IDisposable
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;
    private readonly Mock<ICountryEntityValidator> _mockValidator;

    public AddCommandHandlerTests()
    {
        _mapper = TestHelper.CreateMapper();
        _context = TestHelper.CreateContext();
        _mockValidator = new Mock<ICountryEntityValidator>();
        TestHelper.SeedStubCountries(_context, 0);
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
        var handler = new AddCommandHandler(_context, _mockValidator.Object, _mapper);
        var command = new AddCommand<AddCountryRequest, CountryResponse> { Request = new AddCountryRequest { Name = "Test Country" } };

        _mockValidator.Setup(x => x.ValidateAllRules(It.IsAny<EntityValidatorType>(), It.IsAny<Country>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act.
        var response = await handler.Handle(command, CancellationToken.None);
        var dbCountry = await _context.Countries.SingleAsync(x => x.Name == command.Request.Name);

        // Assert.
        Assert.Equal(command.Request.Name, response.Name);
        Assert.Equal(command.Request.Name, dbCountry.Name);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenValidationFails()
    {
        // Arrange.
        var handler = new AddCommandHandler(_context, _mockValidator.Object, _mapper);
        var command = new AddCommand<AddCountryRequest, CountryResponse>
        {
            Request = new AddCountryRequest { Name = "Test Country" }
        };

        _mockValidator.Setup(x => x.ValidateAllRules(It.IsAny<EntityValidatorType>(), It.IsAny<Country>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Validation failed"));

        // Act.
        var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

        // Assert.
        Assert.Equal("Validation failed", exception.Message);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCommandIsNull()
    {
        // Arrange.
        var handler = new AddCommandHandler(_context, _mockValidator.Object, _mapper);

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