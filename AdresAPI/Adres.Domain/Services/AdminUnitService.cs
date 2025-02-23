using Adres.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public class AdminUnitService : IAdminUnitService
    {
        private readonly AdresDbContext _dbContext;

        public AdminUnitService(AdresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AdminUnit> CreateAdminUnit(AdminUnit adminUnit)
        {
            try
            {
                await _dbContext.AdminUnits.AddAsync(adminUnit);
                await _dbContext.SaveChangesAsync();
                return adminUnit;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AdminUnit> DeleteAdminUnit(int adminUnitId)
        {
            try
            {
                var adminUnitToDelete = await _dbContext.AdminUnits
                    .FirstOrDefaultAsync(au => au.AdminUnitID == adminUnitId);

                if (adminUnitToDelete == null)
                {
                    throw new Exception("AdminUnit to delete not found");
                }

                _dbContext.AdminUnits.Remove(adminUnitToDelete);
                await _dbContext.SaveChangesAsync();

                return adminUnitToDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<IEnumerable<AdminUnit>> GetAllAdminUnits()
        {
            try
            {
                return await _dbContext.AdminUnits.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AdminUnit> GetByID(int adminUnitId)
        {
            try
            {
                return await _dbContext.AdminUnits
                    .FirstOrDefaultAsync(au => au.AdminUnitID == adminUnitId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AdminUnit> UpdateAdminUnit(AdminUnit adminUnit)
        {
            try
            {
                var existingAdminUnit = await _dbContext.AdminUnits
                    .FirstOrDefaultAsync(au => au.AdminUnitID == adminUnit.AdminUnitID);

                if (existingAdminUnit == null)
                {
                    throw new Exception("AdminUnit to update not found");
                }


                existingAdminUnit.Name = adminUnit.Name;
                existingAdminUnit.ReferenceCode = adminUnit.ReferenceCode;

                await _dbContext.SaveChangesAsync();

                return existingAdminUnit;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }
    }
}
