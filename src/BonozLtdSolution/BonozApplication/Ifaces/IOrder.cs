namespace BonozApplication.Ifaces
{
    public interface IOrder
    {
        bool CreateOrder(Order order, int cardId);
        Task<IList<OrderDTO>> GetAllOrders(AllOrdersDTO allOrdersDTO);
        Task<IList<OrderDTO>> GetOrdersByUser(int userId);
        Task<IList<OrderDetailsDTO>> GetOrdersDetails(int id);
        Task<Order> GetOrderById(int orderId);
        Task<bool> DeleteOrder(int orderId);
        Task<bool> UpdateOrder(Order order);
    }
}
