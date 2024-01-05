namespace BonozWeb.Pages
{
    public class DisplayProductBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}