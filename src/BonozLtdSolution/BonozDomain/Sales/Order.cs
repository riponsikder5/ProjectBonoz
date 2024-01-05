namespace BonozDomain.Sales
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public decimal TotalAmount { get; set; }
        public string PaymentDescription { get; set; }

        public virtual IList<OrderDetails> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        [Display(Name = "Ordered")]
        Ordered = 1,
        [Display(Name = "Pickuped By Courier")]
        PickupedByCourier = 2,
        [Display(Name = "Ready For Delivery")]
        ReadyForDelivery = 3,
        [Display(Name = "Delivery Successful")]
        DeliveryCompleted = 4,
        [Display(Name = "Order Cancle")]
        OrderCancle = 5,
        [Display(Name = "Other Issue")]
        OtherIssue = 6,
    }
}