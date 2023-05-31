using PledgeVault.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts;

public interface ICountryService : IService<Country>, IDisposable
{
    Task<ICollection<Country>> GetByNameAsync(string name);
}