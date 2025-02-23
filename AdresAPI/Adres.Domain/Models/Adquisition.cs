using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Adres.Domain.Models
{
    public class Adquisition
    {
        public int AdquisitionID { get; set; }
        public int AdminUnitID { get; set; }
        public int AssetServiceTypeID { get; set; }
        public int ProviderID { get; set; }
        public decimal Budget { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime AdquisitionDate { get; set; }
        public string Documentation { get; set; }
        public bool IsActive { get; set; }

        public AdminUnit AdminUnit { get; set; }
        public AssetServiceType AssetServiceType { get; set; }
        public Provider Provider { get; set; }
        public ICollection<AdquisitionHistory> Histories { get; set; }
    }
}
