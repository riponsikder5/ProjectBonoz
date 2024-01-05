using BonozDomain.AppUser;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BonozWeb.Pages
{
    public class AddEditAddressBase : ComponentBase
    {

        [Parameter]
        public int? Id { get; set; }
        public int UserId { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject] 
        public IUserService UserService { get; set; }

        public AddressDTO AddressDTO { get; set; }

        public string btnText = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            AddressDTO = new AddressDTO();

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
                           
                        }
                    }
                }
            }
            else
            {
                NavigationManager.NavigateTo("/login");
            }

            btnText = Id == null ? "Save" : "Update";
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
                AddressDTO = await UserService.GetAddress((int)Id);
        }

        public async Task HandleSubmit()
        {
            if (Id == null)
            {
                AddressDTO.UserId = UserId;
                await UserService.CreateAddress(AddressDTO);
            }
            else
            {
                await UserService.UpdateAddress(AddressDTO);
            }
        }
    }
}