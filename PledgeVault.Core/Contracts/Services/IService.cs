using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Contracts.Services;

public interface IService<in TAdd, in TUpdate, TResponse>
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