using Adres.Domain.Models;

namespace AdresAPI.DTO
{
    public class AdquisitionHistoryInputDTO
    {
        public int AdquisitionID { get; set; }
        public string Operation { get; set; }
        public string Model { get; set; }

    }
}
