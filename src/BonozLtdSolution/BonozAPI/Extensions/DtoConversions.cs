using BonozDomain.AppUser;

namespace ShopOnline.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductCategoryDTO> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
        {
            return (from productCategory in productCategories
                    select new ProductCategoryDTO
                    {
                        Id = productCategory.Id,
                        Name = productCategory.Name,
                        IconCSS = productCategory.IconCSS
                    }).ToList();
        }
        public static IEnumerable<ProductDTO> ConvertToDto(this IEnumerable<Product> products)
        {
            return (from product in products
                    select new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CategoryId = product.ProductCategory.Id,
                        CategoryName = product.ProductCategory.Name
                    }).ToList();

        }

        public static ProductDTO ConvertToDto(this Product product)

        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.ProductCategory.Id,
                CategoryName = product.ProductCategory.Name,
                ShopId = product.ShopId,


            };

        }

        public static ProductCategoryDTO ConvertToDto(this ProductCategory category)
        {
            return new ProductCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                IconCSS = category.IconCSS,
            };

        }

        public static ProductCategory ConvertToEntity(this ProductCategoryDTO category)
        {
            return new ProductCategory
            {
                Id = category.Id,
                Name = category.Name,
                IconCSS = category.IconCSS,
            };

        }


        public static AddressDTO ConvertToDto(this Address address)
        {
            return new AddressDTO
            {
                Id = address.Id,
                HouseNumber = address.HouseNumber,
                District = address.District,
                Division = address.Division,
                Village = address.Village,
                PoliceStation = address.PoliceStation,
                RoadNumber = address.RoadNumber,
                UserId = address.UserId,
            };

        }

        public static OrderDTO ConvertToDto(this Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                PaymentDescription = order.PaymentDescription,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId,
            };

        }


        public static Address ConvertToEntity(this AddressDTO address)
        {
            return new Address
            {
                Id = address.Id,
                HouseNumber = address.HouseNumber,
                District = address.District,
                Division = address.Division,
                Village = address.Village,
                PoliceStation = address.PoliceStation,
                RoadNumber = address.RoadNumber,
                UserId = address.UserId,
            };

        }


        public static Order ConvertToEntity(this OrderDTO order)
        {
            IList<OrderDetails> OrderDetails = new List<OrderDetails>();

            if(order.OrderDetails != null)
            {
                foreach (var item in order.OrderDetails)
                {
                    var data = new OrderDetails
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                    };

                    OrderDetails.Add(data);
                }
            }
       

            return new Order
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderItems = OrderDetails,
                PaymentDescription = order.PaymentDescription,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId,
                User = order.User,
            };

        }

        public static Product ConvertToEntity(this ProductDTO product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductCategoryId = product.CategoryId,
                ShopId = product.ShopId,
            };
        }

        public static IEnumerable<CartItemDTO> ConvertToDto(this IEnumerable<CartItem> cartItems,
                                                            IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDTO
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Quantity = cartItem.Quantity,
                        TotalPrice = product.Price * cartItem.Quantity
                    }).ToList();
        }
        public static CartItemDTO ConvertToDto(this CartItem cartItem,
                                                    Product product)
        {
            return new CartItemDTO
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Quantity = cartItem.Quantity,
                TotalPrice = product.Price * cartItem.Quantity
            };
        }

    }
}
