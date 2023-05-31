using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Models;

namespace PledgeVault.Core.Contracts;

public interface IPartyService : IService<Party>, IDisposable
{
    Task<ICollection<Party>> GetByCountryIdAsync(int id);

    Task<ICollection<Party>> GetByNameAsync(string name);
}