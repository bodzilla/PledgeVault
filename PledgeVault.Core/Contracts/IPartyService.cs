using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Contracts;

public interface IPartyService : IService<Party, AddPartyRequest, UpdatePartyRequest>, IDisposable
{
    Task<ICollection<Party>> GetByCountryIdAsync(int id);

    Task<ICollection<Party>> GetByNameAsync(string name);
}