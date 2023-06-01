using PledgeVault.Core.Models;
using System;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Contracts;

public interface IPoliticianService : IService<Politician, AddPoliticianRequest, UpdatePoliticianRequest>, IDisposable
{
}