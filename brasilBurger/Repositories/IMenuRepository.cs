using brasilBurger.Models;

namespace brasilBurger.Repositories
{
    public interface IMenuRepository
    {
        Task<Menu?> GetByIdAsync(int id);
        Task<List<Menu>> GetAllAsync();
        Task<List<Menu>> GetActifsAsync();
        Task<Menu> AddAsync(Menu menu);
        Task<Menu> UpdateAsync(Menu menu);
        Task<bool> DeleteAsync(int id);
    }
}
