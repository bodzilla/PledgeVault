using PledgeVault.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Contracts;

public interface ICountryService : IService<Country, AddCountryRequest, UpdateCountryRequest>, IDisposable
{
    Task<ICollection<Country>> GetByNameAsync(string name);
}