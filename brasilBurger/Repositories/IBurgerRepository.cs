using brasilBurger.Models;

namespace brasilBurger.Repositories
{
    public interface IBurgerRepository
    {
        Task<Burger?> GetByIdAsync(int id);
        Task<List<Burger>> GetAllAsync();
        Task<List<Burger>> GetActifsAsync();
        Task<Burger> AddAsync(Burger burger);
        Task<Burger> UpdateAsync(Burger burger);
        Task<bool> DeleteAsync(int id);
    }
}
