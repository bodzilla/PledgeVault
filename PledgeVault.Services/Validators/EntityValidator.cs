﻿using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Validators;

/// <summary>
/// The base entity validator.
/// </summary>
internal static class EntityValidator
{
    public static async Task EnsureCommentExists(PledgeVaultContext context, int commentId, CancellationToken cancellationToken)
        => await EnsureEntityExists(context.Comments, commentId, cancellationToken);

    public static async Task EnsureCountryExists(PledgeVaultContext context, int countryId, CancellationToken cancellationToken)
        => await EnsureEntityExists(context.Countries, countryId, cancellationToken);

    public static async Task EnsurePartyExists(PledgeVaultContext context, int partyId, CancellationToken cancellationToken)
        => await EnsureEntityExists(context.Parties, partyId, cancellationToken);

    public static async Task EnsurePledgeExists(PledgeVaultContext context, int pledgeId, CancellationToken cancellationToken)
        => await EnsureEntityExists(context.Pledges, pledgeId, cancellationToken);

    public static async Task EnsurePoliticianExists(PledgeVaultContext context, int politicianId, CancellationToken cancellationToken)
        => await EnsureEntityExists(context.Politicians, politicianId, cancellationToken);

    public static async Task EnsureResourceExists(PledgeVaultContext context, int resourceId, CancellationToken cancellationToken)
        => await EnsureEntityExists(context.Resources, resourceId, cancellationToken);

    public static async Task EnsureUserExists(PledgeVaultContext context, int userId, CancellationToken cancellationToken)
        => await EnsureEntityExists(context.Users, userId, cancellationToken);

    private static async Task EnsureEntityExists<T>(IQueryable<T> queryable, int id, CancellationToken cancellationToken)
    where T : class, IEntity
    {
        if (!await queryable
                .AsNoTracking()
                .WithOnlyActiveEntities()
                .AnyAsync(x => x.Id == id, cancellationToken))
            throw new EntityValidationException($"{typeof(T).Name} not found with {nameof(id)}: '{id}'.");
    }
}