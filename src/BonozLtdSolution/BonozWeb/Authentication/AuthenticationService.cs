using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace BonozWeb.Authentication
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public AuthenticationService(ProtectedLocalStorage protectedLocalStorage, HttpClient httpClient)
        {
            _protectedLocalStorage = protectedLocalStorage;
            _httpClient = httpClient;

        }

        public async Task<LoggedInUser?> LoginUserAsync(LoginModel loginModel)
        {
            var loggedInUser = await _httpClient.PostAsJsonAsync("https://localhost:44314/api/Account", loginModel);

            if (loggedInUser.IsSuccessStatusCode)
            {
                var result = await loggedInUser.Content.ReadFromJsonAsync<LoggedInUser>();
                await SaveUserToBrowserStorageAsync(result);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<LoggedInUser?> RegisterAsync(RegisterDTO registerDTO)
        {
            var loggedInUser = await _httpClient.PostAsJsonAsync("https://localhost:44314/api/Account/register", registerDTO);

            if (loggedInUser.IsSuccessStatusCode)
            {
                var result = await loggedInUser.Content.ReadFromJsonAsync<LoggedInUser>();
                await SaveUserToBrowserStorageAsync(result);
                return result;
            }
            else
            {
                return null;
            }
        }



        private const string UserStorageKey = "blg_user";
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {

        };
        public async Task SaveUserToBrowserStorageAsync(LoggedInUser user) =>
            await _protectedLocalStorage.SetAsync(UserStorageKey, JsonSerializer.Serialize(user, _jsonSerializerOptions));

        public async Task<LoggedInUser?> GetUserFromBrowserStorageAsync()
        {
            try
            {
                var result = await _protectedLocalStorage.GetAsync<string>(UserStorageKey);
                if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
                {
                    var loggedInUser = JsonSerializer.Deserialize<LoggedInUser>(result.Value, _jsonSerializerOptions);
                    return loggedInUser;
                }
            }
            catch (InvalidOperationException)
            {
                // Eat out this exception
                // as we know this will occure when this method is being called from server
                // Where there is no Browser and LocalStorage
                // Dont worry about this, as this will be called from client side(Browser's side) after this
                // So we will have the data there
                // So we are good to ignore this exception
            }
            return null;
        }

        public async Task RemoveUserFromBroserStorageAsync() =>
            await _protectedLocalStorage.DeleteAsync(UserStorageKey);
    }
}
