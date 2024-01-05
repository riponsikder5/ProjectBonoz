using BonozDomain.Sales;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BonozWeb.Pages
{
    public class AllOrdersBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }
        public string ErrorMessage { get; set; }
        public bool Popup { get; set; }
        public bool DeletePopup { get; set; }

        public IList<OrderDTO> Orders = new List<OrderDTO>();
        public AllOrdersDTO AllOrdersDTO { get; set; }
        public OrderDTO OrderDTO { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AllOrdersDTO = new AllOrdersDTO();
            Orders = await OrderService.GetAllOrders(AllOrdersDTO);
        }

        public async Task HandleSubmit()
        {
            Orders = await OrderService.GetAllOrders(AllOrdersDTO);
            AllOrdersDTO = new AllOrdersDTO();
        }

        public string GetDisplayName(OrderStatus status)
        {
            var fieldInfo = status.GetType().GetField(status.ToString());
            var displayAttribute = fieldInfo.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
            return displayAttribute?.Name ?? status.ToString();
        }

        public async Task GetOrder(int orderId)
        {
            OrderDTO = await OrderService.GetOrder(orderId);
            Popup = true;
        }

        public async Task EditOrder()
        {
            AllOrdersDTO = new AllOrdersDTO();
            await OrderService.UpdateOrder(OrderDTO);
            Orders = await OrderService.GetAllOrders(AllOrdersDTO);
            Popup = false;
        }

        public async Task DeleteOrder()
        {
            AllOrdersDTO = new AllOrdersDTO();
            DeletePopup = false;
            await OrderService.DeleteOrder(OrderDTO.Id);
            Orders = await OrderService.GetAllOrders(AllOrdersDTO);
        }

        public async Task ShowDeletePopup(int id)
        {
            OrderDTO = await OrderService.GetOrder(id);
            DeletePopup = true;
        }

        public void HidePopup()
        {
            Popup = false;
            DeletePopup = false;
        }
    }
}
