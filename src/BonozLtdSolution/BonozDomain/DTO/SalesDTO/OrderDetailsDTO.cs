namespace BonozDomain.DTO.SalesDTO
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageURL { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}