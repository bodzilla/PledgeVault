using PledgeVault.Core.Models;
using System;

namespace PledgeVault.Core.Contracts;

public interface IPledgeService : IService<Pledge>, IDisposable
{
}