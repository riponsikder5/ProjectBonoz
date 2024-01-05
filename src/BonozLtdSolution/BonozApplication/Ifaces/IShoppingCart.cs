namespace BonozApplication.Ifaces
{
    public interface IShoppingCart
    {
        Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDto, int userId);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IList<CartItem>> GetItems(int userId);

        #region HIDE
        //IList<ProductCategoryDTO> ConvertToDTO(IList<ProductCategory> productCategories);
        //IList<ProductDTO> ConvertToDTO(IList<Product> products);
        //ProductDTO ConvertToDTO(Product product);
        //IList<CartItemDTO> ConvertToDTO(IList<CartItem> cartItems, IList<Product> products);
        //CartItemDTO ConvertToDTO(CartItem cartItem, Product product);

        #endregion HIDE
    }
}
