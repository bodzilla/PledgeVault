using MediatR;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Services.Commands;

public sealed class SetInactiveCommand<T> : IRequest<T> where T : IResponse
{
    public int Id { get; set; }
}