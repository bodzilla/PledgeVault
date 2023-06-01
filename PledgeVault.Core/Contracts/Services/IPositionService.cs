using System;
using PledgeVault.Core.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Core.Contracts.Services;

public interface IPositionService : IService<AddPositionRequest, UpdatePositionRequest, PositionResponse>, IDisposable
{
    Task<ICollection<PositionResponse>> GetByTitleAsync(string title);
}