using Adres.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public class AdquisitionHistoryService: IAdquisitionHistoryService
    {
        private readonly AdresDbContext _dbContext;

        public AdquisitionHistoryService(AdresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AdquisitionHistory> CreateAdquisitionHistory(AdquisitionHistory history)
        {
            try
            {
                await _dbContext.AdquisitionHistories.AddAsync(history);
                await _dbContext.SaveChangesAsync();
                return history;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AdquisitionHistory> DeleteAdquisitionHistory(int historyId)
        {
            try
            {
                var historyToDelete = await _dbContext.AdquisitionHistories
                    .FirstOrDefaultAsync(h => h.AdquisitionHistoryID == historyId);

                if (historyToDelete == null)
                {
                    throw new Exception("AdquisitionHistory to delete not found");
                }

                _dbContext.AdquisitionHistories.Remove(historyToDelete);
                await _dbContext.SaveChangesAsync();

                return historyToDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<IEnumerable<AdquisitionHistory>> GetAllAdquisitionHistories()
        {
            try
            {
                return await _dbContext.AdquisitionHistories.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AdquisitionHistory> GetByID(int historyId)
        {
            try
            {
                return await _dbContext.AdquisitionHistories.FirstOrDefaultAsync(h => h.AdquisitionHistoryID == historyId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AdquisitionHistory> UpdateAdquisitionHistory(AdquisitionHistory history)
        {
            try
            {
                var existingHistory = await _dbContext.AdquisitionHistories
                    .FirstOrDefaultAsync(h => h.AdquisitionHistoryID == history.AdquisitionHistoryID);

                if (existingHistory == null)
                {
                    throw new Exception("AdquisitionHistory to update not found");
                }

                existingHistory.AdquisitionID = history.AdquisitionID;
                existingHistory.Operation = history.Operation;
                existingHistory.TimeStamp = history.TimeStamp;
                existingHistory.Model = history.Model;

                await _dbContext.SaveChangesAsync();

                return existingHistory;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }
    }
}
