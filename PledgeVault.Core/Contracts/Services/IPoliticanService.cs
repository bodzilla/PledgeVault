using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Services;

public interface IPoliticianService : IService<AddPoliticianRequest, UpdatePoliticianRequest, PoliticianResponse>, IDisposable
{
    Task<ICollection<PoliticianResponse>> GetByPartyIdAsync(int id);

    Task<ICollection<PoliticianResponse>> GetByNameAsync(string name);
}