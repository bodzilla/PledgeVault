using System;
using PledgeVault.Core.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Core.Contracts;

public interface IPoliticianService : IService<AddPoliticianRequest, UpdatePoliticianRequest, PoliticianResponse>, IDisposable
{
    Task<ICollection<PoliticianResponse>> GetByPartyIdAsync(int id);

    Task<ICollection<PoliticianResponse>> GetByNameAsync(string name);
}