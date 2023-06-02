using MediatR;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Services.Queries;

public sealed class GetByIdQuery<T> : IRequest<T> where T : IResponse
{
    public int Id { get; set; }
}