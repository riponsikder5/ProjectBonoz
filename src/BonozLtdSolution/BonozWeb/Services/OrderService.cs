namespace BonozWeb.Services
{
    public class OrderService : IOrderService
    {

        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task CreateOrder(OrderDTO orderDTO, int cardId)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"https://localhost:44314/api/Order/{cardId}", orderDTO);

                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("/Orders");
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }

        public async Task UpdateOrder(OrderDTO orderDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44314/api/Order/{orderDTO.Id}", orderDTO);
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("AllOrders");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Order not found.");
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }

        public async Task<OrderDTO> GetOrder(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/Order/GetOrderById/{orderId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new OrderDTO();
                    }

                    return await response.Content.ReadFromJsonAsync<OrderDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task DeleteOrder(int orderId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:44314/api/Order/{orderId}");

                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("/AllOrders");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Order not found.");
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }

        public async Task<IList<OrderDTO>> GetAllOrders(AllOrdersDTO allOrdersDTO)
        {

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44314/api/Order", allOrdersDTO);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new List<OrderDTO>();
                    }

                    return await response.Content.ReadFromJsonAsync<IList<OrderDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IList<OrderDTO>> GetOrders(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/Order/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new List<OrderDTO>();
                    }

                    return await response.Content.ReadFromJsonAsync<IList<OrderDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IList<OrderDetailsDTO>> GetOrdersDetails(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/Order/OrdersDetails/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new List<OrderDetailsDTO>();
                    }

                    return await response.Content.ReadFromJsonAsync<IList<OrderDetailsDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

    }
}