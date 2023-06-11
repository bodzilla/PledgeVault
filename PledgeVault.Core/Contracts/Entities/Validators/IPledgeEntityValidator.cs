﻿using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities.Validators;

/// <summary>
/// <inheritdoc cref="IEntityValidator{T}"/>
/// </summary>
public interface IPledgeEntityValidator : IEntityValidator<Pledge>
{
    Task EnsureTitleWithDatePledgedWithPoliticianIdIsUnique(Pledge entity, CancellationToken cancellationToken);
}