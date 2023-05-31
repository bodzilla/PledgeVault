using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Base;

namespace PledgeVault.Core.Contracts;

public interface IService<T> where T : EntityBase
{
    Task<ICollection<T>> GetAllAsync();

    Task<T> GetAsync(int id);

    Task<T> AddAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> SetInactiveAsync(int id);
}