using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Models
{
    public class AdminUnit
    {
        public int AdminUnitID { get; set; }
        public string Name { get; set; }
        public string ReferenceCode { get; set; }


        public ICollection<Adquisition> Adquisitions { get; set; }
    }
}
