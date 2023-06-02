using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Services;

public interface IPartyService : IService<AddPartyRequest, UpdatePartyRequest, PartyResponse>, IDisposable
{
    Task<ICollection<PartyResponse>> GetByCountryIdAsync(int id);

    Task<ICollection<PartyResponse>> GetByNameAsync(string name);
}