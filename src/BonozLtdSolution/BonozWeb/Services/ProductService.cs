using System.Net;

namespace BonozWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public ProductService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        #region Product

        public async Task CreateProduct(ProductDTO product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44314/api/Products", product);
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("ProductList");
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

        public async Task UpdateProduct(ProductDTO product)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44314/api/Products/{product.Id}", product);
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("ProductList");
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

        public async Task DeleteProduct(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:44314/api/Products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("ProductList");
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

        public async Task<ProductDTO> GetProduct(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/Products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<ProductDTO>();
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

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            try
            {
                var response = await _httpClient.GetAsync(new Uri("https://localhost:44314/api/Products"));
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDTO>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
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

        public async Task<IEnumerable<ProductDTO>> GetProductByCategory(int categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/Products/{categoryId}/GetItemsByCategory");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
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
                var response = await _httpClient.GetAsync("https://localhost:44314/api/Products/GetShops");
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
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        #endregion

        #region Product Category

        public async Task CreateCategory(ProductCategoryDTO productCategory)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44314/api/ProductCategories", productCategory);

                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("ProductCategory");
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

        public async Task UpdateCategory(ProductCategoryDTO productCategory)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44314/api/ProductCategories/{productCategory.Id}", productCategory);

                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("ProductCategory");
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

        public async Task<IEnumerable<ProductCategoryDTO>> GetProductCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:44314/api/Products/GetProductCategories");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductCategoryDTO>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<ProductCategoryDTO> GetCategory(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44314/api/ProductCategories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductCategoryDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<ProductCategoryDTO>();
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

        public async Task DeleteCategory(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:44314/api/ProductCategories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("ProductCategory");
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

        #endregion

    }
}
