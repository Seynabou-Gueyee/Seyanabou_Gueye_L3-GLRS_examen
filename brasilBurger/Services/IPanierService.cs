using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface IPanierService
    {
        Task<List<PanierItem>> GetPanierAsync(int clientId);
        Task<bool> AddToPanierAsync(int clientId, string type, int itemId, int quantite = 1);
        Task<bool> UpdateQuantiteAsync(int clientId, string type, int itemId, int quantite);
        Task<bool> RemoveFromPanierAsync(int clientId, string type, int itemId);
        Task<bool> ClearPanierAsync(int clientId);
        Task<decimal> GetPanierTotalAsync(int clientId);
        Task<int> GetPanierCountAsync(int clientId);
    }
}
