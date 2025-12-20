using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface IBurgerService
    {
        Task<List<Burger>> GetAllAsync();
        Task<List<Burger>> GetActifsAsync();
        Task<Burger?> GetByIdAsync(int id);
        Task<Burger> CreateAsync(Burger burger);
        Task<Burger> UpdateAsync(Burger burger);
        Task<bool> ArchiverAsync(int id);
    }
}
