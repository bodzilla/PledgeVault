﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Core.Contracts;

public interface ICountryService : IService<AddCountryRequest, UpdateCountryRequest, CountryResponse>, IDisposable
{
    Task<ICollection<CountryResponse>> GetByNameAsync(string name);
}