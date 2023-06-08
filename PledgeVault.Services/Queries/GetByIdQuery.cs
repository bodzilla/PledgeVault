using MediatR;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Services.Queries;

public sealed record GetByIdQuery<T> : IRequest<T> where T : IResponse
{
    public int Id { get; init; }
}