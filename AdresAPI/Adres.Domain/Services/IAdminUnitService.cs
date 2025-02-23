using Adres.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public interface IAdminUnitService
    {
        public Task<IEnumerable<AdminUnit>> GetAllAdminUnits();
        public Task<AdminUnit> GetByID(int AdmiUnitID);
        public Task<AdminUnit> CreateAdminUnit(AdminUnit adminUnit);
        public Task<AdminUnit> UpdateAdminUnit(AdminUnit adminUnit);
        public Task<AdminUnit> DeleteAdminUnit(int adminUnitID);

    }
}
