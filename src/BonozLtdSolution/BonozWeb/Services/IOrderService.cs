namespace BonozWeb.Services
{
    public interface IOrderService
    {
        Task CreateOrder(OrderDTO orderDTO, int cardId);
        Task UpdateOrder(OrderDTO orderDTO);
        Task<OrderDTO> GetOrder(int orderId);
        Task DeleteOrder(int orderId);
        Task<IList<OrderDTO>> GetAllOrders(AllOrdersDTO allOrdersDTO);
        Task<IList<OrderDTO>> GetOrders(int userId);
        Task<IList<OrderDetailsDTO>> GetOrdersDetails(int id);
    }
}
