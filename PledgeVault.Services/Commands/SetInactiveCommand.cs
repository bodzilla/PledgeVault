using MediatR;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Services.Commands;

public sealed record SetInactiveCommand<T> : IRequest<T> where T : IResponse
{
    public int Id { get; init; }
}