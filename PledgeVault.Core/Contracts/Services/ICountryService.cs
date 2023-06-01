using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Contracts.Services;

public interface ICountryService : IService<AddCountryRequest, UpdateCountryRequest, CountryResponse>, IDisposable
{
    Task<ICollection<CountryResponse>> GetByNameAsync(string name);

    Task<ICollection<CountryResponse>> GetByGovernmentTypeAsync(GovernmentType type);
}