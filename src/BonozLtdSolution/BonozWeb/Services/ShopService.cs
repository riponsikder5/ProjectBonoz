using System.Net;

namespace BonozWeb.Services
{
    public class ShopService : IShopService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public ShopService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        #region Shop

        public async Task CreateShop(Shop Shop)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44314/api/Shop", Shop);
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("/");
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

        public async Task UpdateShop(Shop Shop)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44314/api/Shop/{Shop.Id}", Shop);
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("ShopList");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Shop not found.");
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

        //public async Task DeleteShop(int id)
        //{
        //    try
        //    {
        //        var response = await _httpClient.DeleteAsync($"https://localhost:44314/api/Shops/{id}");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            _navigationManager.NavigateTo("ShopList");
        //        }
        //        else if (response.StatusCode == HttpStatusCode.NotFound)
        //        {
        //            throw new Exception("Category not found.");
        //        }
        //        else
        //        {
        //            var message = await response.Content.ReadAsStringAsync();
        //            throw new Exception($"Http status code: {response.StatusCode} message: {message}");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // Log exception
        //        throw;
        //    }
        //}

        public async Task<Shop> GetShop(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/Shop/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(Shop);
                    }

                    return await response.Content.ReadFromJsonAsync<Shop>();
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

        public async Task<IEnumerable<Shop>> GetShops()
        {
            try
            {
                var response = await _httpClient.GetAsync(new Uri("https://localhost:44314/api/Shop"));
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<Shop>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<Shop>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine($"Error in GetItems(): {ex}");
                throw;
            }
        }

        #endregion
    }
}