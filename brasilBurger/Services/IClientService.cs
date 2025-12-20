using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface IClientService
    {
        Task<Client?> GetByIdAsync(int id);
        Task<Client?> GetByEmailAsync(string email);
        Task<Client?> AuthenticateAsync(string email, string password);
        Task<Client> CreateAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task<int> GetTotalCommandesAsync(int clientId);
        Task<decimal> GetTotalDepensesAsync(int clientId);
        Task<int> GetTotalLivraisonsAsync(int clientId);
    }
}
