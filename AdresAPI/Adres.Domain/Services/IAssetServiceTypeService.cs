using Adres.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public interface IAssetServiceTypeService
    {
        public Task<IEnumerable<AssetServiceType>> GetAllAssetServiceType();
        public Task<AssetServiceType> GetByID(int AssetServiceTypeID);
        public Task<AssetServiceType> CreateAssetServiceType(AssetServiceType assetServiceType);
        public Task<AssetServiceType> UpdateAssetServiceType(AssetServiceType assetServiceType);
        public Task<AssetServiceType> DeleteAssetServiceType(int AssetServiceTypeID);   


    }
}
