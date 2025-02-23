namespace AdresAPI.DTO
{
    public class AdquisitionResponseDTO
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
    }
}
