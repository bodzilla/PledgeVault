using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Base;

namespace PledgeVault.Core.Contracts;

public interface IService<T, in TAdd, in TUpdate, TResponse>
    where T : EntityBase
    where TAdd : IRequest
    where TUpdate : IRequest
    where TResponse : IResponse
{
    Task<ICollection<TResponse>> GetAllAsync();

    Task<TResponse> GetByIdAsync(int id);

    Task<TResponse> AddAsync(TAdd request);

    Task<TResponse> UpdateAsync(TUpdate request);

    Task<TResponse> SetInactiveAsync(int id);
}