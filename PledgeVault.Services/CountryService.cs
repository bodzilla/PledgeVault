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
    public sealed class CountryService : ICountryService
    {
        private readonly ILogger<ICountryService> _logger;
        private readonly PledgeVaultContext _context;

        public CountryService(ILogger<ICountryService> logger, PledgeVaultContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Dispose() => _context?.Dispose();

        public async Task<ICollection<Country>> GetAllAsync()
        {
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting all {nameof(Country)}");
                throw;
            }
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException($"{nameof(Country.Id)} is invalid", nameof(id));
                return await _context.Countries.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting {nameof(Country)} by id: {id}");
                throw;
            }
        }

        public async Task<bool> IsExistsByIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException($"{nameof(Country.Id)} is invalid", nameof(id));
                return await _context.Countries.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if {nameof(Country)} exists by id: {id}");
                throw;
            }
        }

        public async Task<Country> AddAsync(Country entity)
        {
            try
            {
                ValidateEntity(entity);
                if (entity.Id != 0) throw new ArgumentException($"{nameof(Country.Id)} should not be set", nameof(Country));

                await _context.Countries.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding {nameof(Country)}");
                throw;
            }
        }

        public async Task<Country> UpdateAsync(Country entity)
        {
            try
            {
                ValidateEntity(entity);
                if (entity.Id <= 0) throw new ArgumentException($"{nameof(Country.Id)} is invalid", nameof(entity));

                entity.EntityModified = DateTime.Now;
                _context.Countries.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating {nameof(Country)}");
                throw;
            }
        }

        public async Task<Country> SetInactiveAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException($"{nameof(Country.Id)} is invalid", nameof(id));

                var entity = await _context.Countries.FindAsync(id) ?? throw new ArgumentException($"{nameof(Country)} not found", nameof(id));

                entity.EntityActive = false;
                entity.EntityModified = DateTime.Now;
                _context.Countries.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while setting {nameof(Country)} inactive");
                throw;
            }
        }

        private static void ValidateEntity(Country entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (String.IsNullOrWhiteSpace(entity.Name)) throw new ArgumentException($"{nameof(Country.Name)} is invalid", nameof(entity));
            if (entity.DateEstablished.HasValue && entity.DateEstablished.Value > DateTime.Now) throw new ArgumentException($"{nameof(Country.DateEstablished)} is invalid", nameof(entity));
        }
    }
}
