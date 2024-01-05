namespace BonozWeb.Services
{
    public interface IAccountService
    {
        Task Register(RegisterDTO orderDTO);
    }
}