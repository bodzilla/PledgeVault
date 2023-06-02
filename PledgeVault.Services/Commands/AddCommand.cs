using MediatR;

namespace PledgeVault.Services.Commands;

public sealed class AddCommand<TRequest, TResponse> : IRequest<TResponse>
{
    public TRequest Request { get; set; }
}
