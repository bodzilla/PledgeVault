using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Services;

public interface IPledgeService : IService<AddPledgeRequest, UpdatePledgeRequest, PledgeResponse>, IDisposable
{
    Task<ICollection<PledgeResponse>> GetByPoliticianIdAsync(int id);

    Task<ICollection<PledgeResponse>> GetByTitleAsync(string title);
}