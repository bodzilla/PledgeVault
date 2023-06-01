using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Core.Contracts;

public interface IPledgeService : IService<AddPledgeRequest, UpdatePledgeRequest, PledgeResponse>, IDisposable
{
    Task<ICollection<PledgeResponse>> GetByPoliticianIdAsync(int id);

    Task<ICollection<PledgeResponse>> GetByTitleAsync(string title);
}