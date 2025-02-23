using Adres.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public class AdquisitionService : IAdquisitionService
    {
        private readonly AdresDbContext _dbContext;

        public AdquisitionService(AdresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Adquisition> CreateAdquisition(Adquisition adquisition)
        {
            try
            {
                await _dbContext.Adquisitions.AddAsync(adquisition);
                await _dbContext.SaveChangesAsync();
                return adquisition;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<Adquisition> DeleteAdquisition(int adquisitionId)
        {
            try
            {
                var adquisitionToDelete = await _dbContext.Adquisitions
                    .FirstOrDefaultAsync(a => a.AdquisitionID == adquisitionId);

                if (adquisitionToDelete == null)
                {
                    throw new Exception("Adquisition to delete not found");
                }

                _dbContext.Adquisitions.Remove(adquisitionToDelete);
                await _dbContext.SaveChangesAsync();

                return adquisitionToDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<List<Adquisition>> GetAllAdquisitions()
        {
            try
            {
                return await _dbContext.Adquisitions.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<Adquisition> GetByIDAsync(int adquisitionId)
        {
            try
            {
                return await _dbContext.Adquisitions
                    .FirstOrDefaultAsync(a => a.AdquisitionID == adquisitionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<Adquisition> UpdateAdquisition(Adquisition adquisition)
        {
            try
            {
                var existingAdquisition = await _dbContext.Adquisitions
                    .FirstOrDefaultAsync(a => a.AdquisitionID == adquisition.AdquisitionID);

                if (existingAdquisition == null)
                {
                    throw new Exception("Adquisition to update not found");
                }

                existingAdquisition.AdminUnitID = adquisition.AdminUnitID;
                existingAdquisition.AssetServiceTypeID = adquisition.AssetServiceTypeID;
                existingAdquisition.ProviderID = adquisition.ProviderID;
                existingAdquisition.Budget = adquisition.Budget;
                existingAdquisition.Quantity = adquisition.Quantity;
                existingAdquisition.UnitPrice = adquisition.UnitPrice;
                existingAdquisition.TotalPrice = adquisition.TotalPrice;
                existingAdquisition.AdquisitionDate = adquisition.AdquisitionDate;
                existingAdquisition.Documentation = adquisition.Documentation;
                existingAdquisition.IsActive = adquisition.IsActive;

                await _dbContext.SaveChangesAsync();

                return existingAdquisition;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }
    }
}
