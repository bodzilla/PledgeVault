using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Resources;

public sealed class GetByIdQuery : IRequest<ResourceResponse>
{
    public int Id { get; set; }
}