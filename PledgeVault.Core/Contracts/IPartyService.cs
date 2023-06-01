using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Core.Contracts;

public interface IPartyService : IService<AddPartyRequest, UpdatePartyRequest, PartyResponse>, IDisposable
{
    Task<ICollection<PartyResponse>> GetByCountryIdAsync(int id);

    Task<ICollection<PartyResponse>> GetByNameAsync(string name);
}