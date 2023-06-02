using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Services;

public interface IPositionService : IService<AddPositionRequest, UpdatePositionRequest, PositionResponse>, IDisposable
{
    Task<ICollection<PositionResponse>> GetByTitleAsync(string title);
}