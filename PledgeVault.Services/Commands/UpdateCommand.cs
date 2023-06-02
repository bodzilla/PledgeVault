using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Services.Commands;

public sealed class UpdateCommand<T> : IRequest<T> where T : IResponse
{
    public UpdateCountryRequest Request { get; set; }
}