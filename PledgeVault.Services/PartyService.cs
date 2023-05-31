using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services
{
    public sealed class PartyService : IPartyService
    {
        private readonly ILogger<IPartyService> _logger;
        private readonly PledgeVaultContext _context;

        public PartyService(ILogger<IPartyService> logger, PledgeVaultContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Dispose() => _context?.Dispose();

        public async Task<ICollection<Party>> GetAllAsync()
        {
            try
            {
                return await _context.Parties.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting all {nameof(Party)}");
                throw;
            }
        }

        public async Task<Party> GetByIdAsync(int id)
        {
            try
            {
                ValidateExistingId(id);
                return await _context.Parties.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting {nameof(Party)} by id: {id}");
                throw;
            }
        }

        public async Task<Party> AddAsync(Party entity)
        {
            try
            {
                ValidateEntity(entity);
                ValidateNewId(entity);

                await _context.Parties.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding {nameof(Party)}");
                throw;
            }
        }

        public async Task<Party> UpdateAsync(Party entity)
        {
            try
            {
                ValidateExistingId(entity.Id);
                ValidateEntity(entity);

                entity.EntityModified = DateTime.Now;
                _context.Parties.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating {nameof(Party)}");
                throw;
            }
        }

        public async Task<Party> SetInactiveAsync(int id)
        {
            try
            {
                ValidateExistingId(id);

                var entity = await _context.Parties.FindAsync(id) ?? throw new ArgumentException($"{nameof(Party)} not found", nameof(Party.Id));
                entity.EntityActive = false;
                entity.EntityModified = DateTime.Now;
                _context.Parties.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while setting {nameof(Party)} inactive");
                throw;
            }
        }

        private void ValidateEntity(Party entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (String.IsNullOrWhiteSpace(entity.Name)) throw new ArgumentException($"{nameof(Party.Name)} is invalid", nameof(entity));
            if (entity.DateEstablished.HasValue && entity.DateEstablished.Value > DateTime.Now) throw new ArgumentException($"{nameof(Party.DateEstablished)} is invalid", nameof(entity));

            // Detach related entities to prevent EF from trying to add/update/delete them.
            _context.Entry(entity.Country).State = EntityState.Detached;
            foreach (var party in entity.Politicians) _context.Entry(party).State = EntityState.Detached;
        }

        private static void ValidateNewId(Party entity)
        {
            if (entity?.Id is not 0) throw new ArgumentException($"{nameof(Party.Id)} should not be set", nameof(Party));
        }

        private static void ValidateExistingId(int id)
        {
            if (id <= 0) throw new ArgumentException($"{nameof(Party.Id)} is invalid", nameof(id));
        }
    }
}
