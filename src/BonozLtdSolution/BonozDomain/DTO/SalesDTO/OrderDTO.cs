namespace BonozDomain.DTO.SalesDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentDescription { get; set; }
        public string OrderStatusDisplayName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }

        public virtual IList<OrderDetailsDTO> OrderDetails { get; set; }

        public string FormattedOrderDate => OrderDate.ToString("MMMM d, yyyy");

        public string GetDisplayName(OrderStatus status)
        {
            var fieldInfo = status.GetType().GetField(status.ToString());
            var displayAttribute = fieldInfo.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
            OrderStatusDisplayName = displayAttribute?.Name ?? status.ToString();

            return OrderStatusDisplayName;
        }

    }
}
