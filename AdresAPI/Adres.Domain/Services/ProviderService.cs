using Adres.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public class ProviderService : IProviderService
    {
        private readonly AdresDbContext _dbContext;

        public ProviderService(AdresDbContext context) 
        {
            _dbContext = context;
        }
        public async Task<Provider> CreateProvider(Provider provider)
        {
            try
            {
                await _dbContext.Providers.AddAsync(provider);
                await _dbContext.SaveChangesAsync();
                return provider;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex.ToString());
                return null;
            }
        }

        public async Task<Provider> DeleteProvider(int providerID)
        {
            try
            {
                var providerToDelete = await _dbContext.Providers.FirstOrDefaultAsync(prov => prov.ProviderID == providerID);

                if (providerToDelete == null)
                {
                    throw new Exception("Provider to update not found");
                }

                _dbContext.Providers.Remove(providerToDelete);
                await _dbContext.SaveChangesAsync();

                return providerToDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<Provider>> GetAllProviders()
        {
            try
            {
                return await _dbContext.Providers.ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex.ToString());
                return null;
            }
        }

        public async Task<Provider> GetByID(int providerID)
        {
            try
            {
                return await _dbContext.Providers.FirstOrDefaultAsync(prov => prov.ProviderID == providerID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex.ToString());
                return null;
            }
        }

        public async Task<Provider> UpdateProvider(Provider provider)
        {
            try
            {
                var existingProvider = await _dbContext.Providers.FirstOrDefaultAsync(prov => prov.ProviderID == provider.ProviderID);

                if (existingProvider == null)
                {
                    throw new Exception("Provider to update not found");
                }

                existingProvider.Name = provider.Name;
                existingProvider.ReferenceCode = provider.ReferenceCode;

                await _dbContext.SaveChangesAsync();

                return existingProvider;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex.ToString());
                return null;
            }
            
        }
    }
}
