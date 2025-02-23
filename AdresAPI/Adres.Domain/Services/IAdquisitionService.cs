using Adres.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public interface IAdquisitionService
    {
        public Task<List<Adquisition>> GetAllAdquisitions();
        public Task<Adquisition> GetByIDAsync(int AdquisitionID);
        public Task<Adquisition> CreateAdquisition(Adquisition adquisition);
        public Task<Adquisition> UpdateAdquisition(Adquisition adquistion);
        public Task<Adquisition> DeleteAdquisition(int AdqusisitionID);

    }
}
