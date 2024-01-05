namespace BonozApplication.Managers
{
    public class OrderManager : BaseDataManager, IOrder
    {
        public OrderManager(BanazDbContext model) : base(model)
        {
        }

        public bool CreateOrder(Order order, int CardId)
        {
            try
            {
                _dbContext.Database.BeginTransaction();

                var orderDetailsList = order.OrderItems.ToList();
                order.OrderItems = null;

                AddUpdateEntity(order);
                var orderId = order.Id;

                var orderDetails = orderDetailsList.Select(item =>
                 {
                     item.OrderId = orderId;
                     return item;
                 }).ToList();

                AddUpdateRange(orderDetails);

                var cardItems = _dbContext.CartItems.Where(x => x.CartId == CardId).ToList();
                if (cardItems.Count > 0)
                {
                    _dbContext.CartItems.RemoveRange(cardItems);
                }

                var cardData = _dbContext.Carts.FirstOrDefault(x => x.Id == CardId);
                if (cardData != null)
                {
                    _dbContext.Carts.Remove(cardData);
                }

                foreach (var orderedProduct in orderDetails)
                {
                    var productToUpdate = _dbContext.Products.FirstOrDefault(p => p.Id == orderedProduct.ProductId);

                    if (productToUpdate != null)
                    {
                        productToUpdate.Quantity -= orderedProduct.Quantity;
                    }
                }

                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw;
            }
        }

        public async Task<IList<OrderDetailsDTO>> GetOrdersDetails(int id)
        {
            List<OrderDetailsDTO> orderDetailsList = _dbContext.OrderDetails
                    .Where(o => o.OrderId == id)
                    .Join(
                        _dbContext.Products,
                        od => od.ProductId,
                        p => p.Id,
                        (od, p) => new OrderDetailsDTO
                        {
                            Id = od.Id,
                            OrderId = od.OrderId,
                            ProductId = od.ProductId,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            ProductName = p.Name,
                            ImageURL = p.ImageURL
                        }
                    )
                    .ToList();

            return orderDetailsList;
        }

        public async Task<IList<OrderDTO>> GetOrdersByUser(int userId)
        {
            List<OrderDTO> orderList = _dbContext.Orders
                    .Where(o => o.UserId == userId && o.IsDeleted == false)
                    .Select(o => new OrderDTO
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        Status = o.Status,
                        TotalAmount = o.TotalAmount,
                        PaymentDescription = o.PaymentDescription,
                        UserId = o.UserId
                    })
                    .ToList();

            return orderList;
        }

        public async Task<IList<OrderDTO>> GetAllOrders(AllOrdersDTO allOrdersDTO)
        {
            List<OrderDTO> orderList = _dbContext.Orders
                .Where(o =>
                    (allOrdersDTO.FromDate == null || o.OrderDate >= allOrdersDTO.FromDate) &&
                    (allOrdersDTO.ToDate == null || o.OrderDate <= allOrdersDTO.ToDate) &&
                    (allOrdersDTO.OrderStatus == null || allOrdersDTO.OrderStatus == o.Status) && o.IsDeleted == false
                )
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    PaymentDescription = o.PaymentDescription,
                })
                .ToList();

            return orderList;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                var OrderEntity = await GetOrderById(order.Id);
                if (OrderEntity != null)
                {
                    OrderEntity.Status = order.Status;
                    //OrderEntity.PaymentDescription = order.PaymentDescription;
                    //OrderEntity.TotalAmount = order.TotalAmount;

                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw;
            }
        }
        
        public async Task<bool> DeleteOrder(int orderId)
        {
            try
            {
                var OrderEntity = await GetOrderById(orderId);
                if (OrderEntity != null)
                {
                    OrderEntity.IsDeleted = true;
                    OrderEntity.DeletedDate = DateTime.Now;

                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw;
            }
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            Order order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id ==  orderId && x.IsDeleted == false);
            if(order == null)
            {
                order = new Order();
            }

            return order;
        }
    }
}
