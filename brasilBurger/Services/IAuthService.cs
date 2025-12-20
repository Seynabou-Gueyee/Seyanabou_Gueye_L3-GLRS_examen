using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface IAuthService
    {
        Task<Client?> AuthenticateAsync(string email, string password);
        Task<Client> RegisterAsync(Client client);
        Task<bool> EmailExistsAsync(string email);
        Task<Client?> GetCurrentClientAsync(int clientId);
        Task<bool> ChangePasswordAsync(int clientId, string oldPassword, string newPassword);
    }
}
