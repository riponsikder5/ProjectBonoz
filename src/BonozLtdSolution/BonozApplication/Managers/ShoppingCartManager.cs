namespace BonozApplication.Managers
{
    public class ShoppingCartManager : BaseDataManager, IShoppingCart
    {
        public ShoppingCartManager(BanazDbContext context) : base(context)
        {
        }

        public async Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDto, int userId)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                int cartId;
                var cartExist = GetCartExists(userId);

                if (cartExist == null)
                {
                    var cartObject = new Cart()
                    {
                        UserId = userId,
                    };

                    _dbContext.Carts.Add(cartObject);
                    _dbContext.SaveChanges();
                    cartId = cartObject.Id;
                }
                else
                {
                    cartId = cartExist.Id;
                }

                if (await CartItemExists(cartId, cartItemToAddDto.ProductId) == false)
                {
                    var item = await (from product in _dbContext.Products
                                      where product.Id == cartItemToAddDto.ProductId
                                      select new CartItem
                                      {
                                          CartId = cartId,
                                          ProductId = product.Id,
                                          Quantity = cartItemToAddDto.Quantity
                                      }).SingleOrDefaultAsync();

                    if (item != null)
                    {
                        var result = await _dbContext.CartItems.AddAsync(item);
                        await _dbContext.SaveChangesAsync();
                        _dbContext.Database.CommitTransaction();
                        return result.Entity;
                    }
                }

                _dbContext.Database.CommitTransaction();
                return null;


            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw;
            }
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = FindEntity<CartItem>(id);
            if (item != null)
            {
                _dbContext.CartItems.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
            return item;
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in _dbContext.Carts
                          join cartItem in _dbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IList<CartItem>> GetItems(int userId)
        {
            return await (from cart in _dbContext.Carts
                          join cartItem in _dbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDto)
        {
            var item = FindEntity<CartItem>(id);
            if (item != null)
            {
                item.Quantity = cartItemQtyUpdateDto.Quantity;
                await _dbContext.SaveChangesAsync();
                return item;
            }
            return null;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await _dbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }

        private Cart GetCartExists(int userid)
        {
            return _dbContext.Carts.FirstOrDefault(c => c.UserId == userid);
        }

    }
}
