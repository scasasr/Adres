using Adres.Domain.Models;

namespace AdresAPI.DTO
{
    public class AdquisitionHistoryResponseDTO
    {
        public int AdquisitionHistoryID { get; set; }
        public int AdquisitionID { get; set; }
        public string Operation { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Model { get; set; }

    }
}
