using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface ICatalogueService
    {
        Task<IEnumerable<Burger>> GetAllBurgersAsync();
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<IEnumerable<Complement>> GetAllComplementsAsync();
        Task<Burger?> GetBurgerByIdAsync(int id);
        Task<Menu?> GetMenuByIdAsync(int id);
        Task<Complement?> GetComplementByIdAsync(int id);
        Task<IEnumerable<Burger>> SearchBurgersAsync(string searchTerm);
        Task<IEnumerable<Menu>> SearchMenusAsync(string searchTerm);
    }
}
