using Microsoft.AspNetCore.Components.Forms;

namespace BonozWeb.Pages
{
    public class AddEditProductBase : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }
        public List<ProductCategoryDTO> ProductCategories { get; set; }
        public List<Shop> Shops { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        public ProductDTO ProductDTO { get; set; }

        public string btnText = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            ProductDTO = new ProductDTO()
            {
            };

            ProductCategories = new List<ProductCategoryDTO>();
            Shops = new List<Shop>();

            btnText = Id == null ? "Save" : "Update";
            ProductCategories = (await ProductService.GetProductCategories()).ToList();
            Shops = (await ProductService.GetShops()).ToList();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id == null)
            {
                ProductDTO.CategoryId = ProductCategories[0].Id;
                ProductDTO.ShopId = Shops[0].Id;
            }
            else
            {
                ProductDTO = await ProductService.GetProduct((int)Id);
            }
        }

        public async Task HandleSubmit()
        {
            if (Id == null)
                await ProductService.CreateProduct(ProductDTO);
            else
                await ProductService.UpdateProduct(ProductDTO);
        }

        public async Task DeleteProduct()
        {
            await ProductService.DeleteProduct(ProductDTO.Id);
        }

        public async Task HandleFileSelection(InputFileChangeEventArgs e)
        {
            var file = e.File;

            if (file != null)
            {
                var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var imagesFolderPath = Path.Combine(wwwrootPath, "Images");

                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                var guid = Guid.NewGuid().ToString();
                var fileName = $"{guid}_{file.Name}";
                var filePath = Path.Combine(imagesFolderPath, fileName);

                // Delete the previous image file if it exists
                if (!string.IsNullOrEmpty(ProductDTO.ImageURL))
                {
                    var previousImagePath = Path.Combine(wwwrootPath, ProductDTO.ImageURL.TrimStart('/'));
                    if (File.Exists(previousImagePath))
                    {
                        File.Delete(previousImagePath);
                    }
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.OpenReadStream().CopyToAsync(stream);
                }

                ProductDTO.ImageURL = $"/Images/{fileName}";
            }
        }



    }
}