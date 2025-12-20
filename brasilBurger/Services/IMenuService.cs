using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface IMenuService
    {
        Task<List<Menu>> GetAllAsync();
        Task<List<Menu>> GetActifsAsync();
        Task<Menu?> GetByIdAsync(int id);
        Task<Menu> CreateAsync(Menu menu);
        Task<Menu> UpdateAsync(Menu menu);
        Task<bool> ArchiverAsync(int id);
    }
}
