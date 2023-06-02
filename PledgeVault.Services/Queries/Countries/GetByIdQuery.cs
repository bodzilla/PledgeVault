using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetByIdQuery : IRequest<CountryResponse>
{
    public int Id { get; set; }
}