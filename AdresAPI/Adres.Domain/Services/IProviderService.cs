using Adres.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public interface IProviderService
    {
        public Task<IEnumerable<Provider>> GetAllProviders();
        public Task<Provider> GetByID(int providerID);
        public Task<Provider> CreateProvider(Provider provider); 
        public Task<Provider> UpdateProvider(Provider provider);
        public Task<Provider> DeleteProvider(int providerID);

    }
}
