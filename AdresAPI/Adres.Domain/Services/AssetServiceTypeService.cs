using Adres.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public class AssetServiceTypeService : IAssetServiceTypeService
    {
        private readonly AdresDbContext _dbContext;

        public AssetServiceTypeService(AdresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AssetServiceType> CreateAssetServiceType(AssetServiceType assetServiceType)
        {
            try
            {
                await _dbContext.AssetServiceTypes.AddAsync(assetServiceType);
                await _dbContext.SaveChangesAsync();
                return assetServiceType;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AssetServiceType> DeleteAssetServiceType(int assetServiceTypeId)
        {
            try
            {
                var assetServiceTypeToDelete = await _dbContext.AssetServiceTypes
                    .FirstOrDefaultAsync(ast => ast.AssetServiceTypeID == assetServiceTypeId);

                if (assetServiceTypeToDelete == null)
                {
                    throw new Exception("AssetServiceType to delete not found");
                }

                _dbContext.AssetServiceTypes.Remove(assetServiceTypeToDelete);
                await _dbContext.SaveChangesAsync();

                return assetServiceTypeToDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<IEnumerable<AssetServiceType>> GetAllAssetServiceType()
        {
            try
            {
                return await _dbContext.AssetServiceTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AssetServiceType> GetByID(int assetServiceTypeId)
        {
            try
            {
                return await _dbContext.AssetServiceTypes
                    .FirstOrDefaultAsync(ast => ast.AssetServiceTypeID == assetServiceTypeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }

        public async Task<AssetServiceType> UpdateAssetServiceType(AssetServiceType assetServiceType)
        {
            try
            {
                var existingAssetServiceType = await _dbContext.AssetServiceTypes
                    .FirstOrDefaultAsync(ast => ast.AssetServiceTypeID == assetServiceType.AssetServiceTypeID);

                if (existingAssetServiceType == null)
                {
                    throw new Exception("AssetServiceType to update not found");
                }

              
                existingAssetServiceType.Name = assetServiceType.Name;
                existingAssetServiceType.ReferenceCode = assetServiceType.ReferenceCode;

                await _dbContext.SaveChangesAsync();

                return existingAssetServiceType;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something has gone wrong. Error: " + ex);
                return null;
            }
        }
    }
}
