using PledgeVault.Core.Models;
using System;
using PledgeVault.Core.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts;

public interface IPositionService : IService<Position, AddPositionRequest, UpdatePositionRequest>, IDisposable
{
    Task<ICollection<Position>> GetByTitleAsync(string title);
}