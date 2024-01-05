using BonozDomain.AppUser;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BonozWeb.Pages
{
    public class CheckoutBase : ComponentBase
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<CartItemDTO> ShoppingCartItems { get; set; }

        public OrderDTO OrderDTO = new OrderDTO();
        public IList<OrderDetailsDTO> OrderDetailsDTO = new List<OrderDetailsDTO>();
        public AddressDTO Address = new AddressDTO();

        protected decimal PaymentAmount { get; set; }


        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IOrderService OrderService { get; set; }

        public int UserId {  get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity != null)
                {
                    if (user.Identity.IsAuthenticated)
                    {
                        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        if (!string.IsNullOrEmpty(userIdClaim))
                        {
                            if (int.TryParse(userIdClaim, out int userId))
                            {
                                int userIdInt = int.Parse(userIdClaim);
                                UserId = userIdInt;
                                ShoppingCartItems = await ShoppingCartService.GetItems(userIdInt);
                                PaymentAmount = ShoppingCartItems.Sum(p => p.TotalPrice);
                                Address = await UserService.GetAddress(userIdInt);
                            }
                        }
                    }
                }
                else
                {
                    NavigationManager.NavigateTo("/login");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        protected void UpdatePaymentDescription(char paymentDescription)
        {
            if (paymentDescription == 'b')
            {
                OrderDTO.PaymentDescription = "bKash";
            }
            else if (paymentDescription == 'n')
            {
                OrderDTO.PaymentDescription = "Nagod";
            }
            else if (paymentDescription == 'c')
            {
                OrderDTO.PaymentDescription = "Cash On";
            }
        }

        public async Task HandleSubmit()
        {

            if (Address.Id == 0)
            {

            }
            else
            {
                foreach (var item in ShoppingCartItems)
                {
                    var data = new OrderDetailsDTO
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                    };

                    OrderDetailsDTO.Add(data);
                }

                OrderDTO.OrderDetails = OrderDetailsDTO;
                OrderDTO.Status = OrderStatus.Ordered;
                OrderDTO.TotalAmount = PaymentAmount;
                OrderDTO.OrderDate = DateTime.Now;
                OrderDTO.UserId = UserId;

                int cardId = ShoppingCartItems.FirstOrDefault().CartId;

                await OrderService.CreateOrder(OrderDTO, cardId);
            }
        }

        public void CreateAddress()
        {
            navigationManager.NavigateTo("AddEditAddress");
        }

        public void EditAddress(int userId)
        {
            navigationManager.NavigateTo($"AddEditAddress/{userId}");
        }
    }
}