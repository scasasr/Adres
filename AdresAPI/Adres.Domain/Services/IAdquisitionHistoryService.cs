using Adres.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Services
{
    public interface IAdquisitionHistoryService
    {
        Task<AdquisitionHistory> CreateAdquisitionHistory(AdquisitionHistory history);
        Task<AdquisitionHistory> DeleteAdquisitionHistory(int historyId);
        Task<IEnumerable<AdquisitionHistory>> GetAllAdquisitionHistories();
        Task<AdquisitionHistory> GetByID(int historyId);
        Task<AdquisitionHistory> UpdateAdquisitionHistory(AdquisitionHistory history);
    }
}
