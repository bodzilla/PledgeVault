﻿using MediatR;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Services.Commands;

public sealed class AddCommand<TRequest, TResponse> : IRequest<TResponse>
    where TRequest : Core.Contracts.Dtos.IRequest
    where TResponse : IResponse
{
    public TRequest Request { get; set; }
}
