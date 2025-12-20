using brasilBurger.Models;

namespace brasilBurger.Repositories
{
    public interface IZoneRepository
    {
        Task<Zone?> GetByIdAsync(int id);
        Task<List<Zone>> GetAllAsync();
        Task<Zone> AddAsync(Zone zone);
        Task<Zone> UpdateAsync(Zone zone);
        Task<bool> DeleteAsync(int id);
    }
}
