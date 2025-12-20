using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface IComplementService
    {
        Task<List<Complement>> GetAllAsync();
        Task<List<Complement>> GetActifsAsync();
        Task<List<Complement>> GetByTypeAsync(TypeComplement type);
        Task<Complement?> GetByIdAsync(int id);
        Task<Complement> CreateAsync(Complement complement);
        Task<Complement> UpdateAsync(Complement complement);
        Task<bool> ArchiverAsync(int id);
    }
}
