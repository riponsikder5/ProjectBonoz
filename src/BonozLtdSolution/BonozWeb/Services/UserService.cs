
using BonozDomain.DTO.SalesDTO;

namespace BonozWeb.Services
{
    public class UserService : IUserService
    {

        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public UserService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task CreateAddress(AddressDTO addressDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44314/api/Address", addressDTO);

                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("/Checkout");
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

        public async Task UpdateAddress(AddressDTO addressDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44314/api/Address/{addressDTO.Id}", addressDTO);

                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("/");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Category not found.");
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

        public async Task<AddressDTO> GetAddress(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/Address/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(AddressDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<AddressDTO>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new AddressDTO();
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

        public async Task<IList<User>> GetUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:44314/api/Users/getallusers");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new List<User>();
                    }

                    return await response.Content.ReadFromJsonAsync<IList<User>>();
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