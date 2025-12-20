using brasilBurger.Models;

namespace brasilBurger.Repositories
{
    public interface IComplementRepository
    {
        Task<Complement?> GetByIdAsync(int id);
        Task<List<Complement>> GetAllAsync();
        Task<List<Complement>> GetActifsAsync();
        Task<List<Complement>> GetByTypeAsync(TypeComplement type);
        Task<Complement> AddAsync(Complement complement);
        Task<Complement> UpdateAsync(Complement complement);
        Task<bool> DeleteAsync(int id);
    }
}
