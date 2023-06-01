using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Base;

namespace PledgeVault.Core.Contracts;

public interface IService<T, in TAdd, in TUpdate> where T : EntityBase where TAdd : IRequest where TUpdate : IRequest
{
    Task<ICollection<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task<T> AddAsync(TAdd request);

    Task<T> UpdateAsync(TUpdate request);

    Task<T> SetInactiveAsync(int id);
}