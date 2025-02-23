using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Models
{
    public class AdquisitionHistory
    {
        public int AdquisitionHistoryID { get; set; }
        public int AdquisitionID { get; set; }
        public string Operation { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Model { get; set; }  

 
        public Adquisition Adquisition { get; set; }
    }
}
