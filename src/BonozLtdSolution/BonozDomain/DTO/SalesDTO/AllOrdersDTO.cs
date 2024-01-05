namespace BonozDomain.DTO.SalesDTO
{
    public class AllOrdersDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
