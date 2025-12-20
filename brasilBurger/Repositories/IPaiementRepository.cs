using brasilBurger.Models;

namespace brasilBurger.Repositories
{
    public interface IPaiementRepository
    {
        Task<Paiement?> GetByIdAsync(int id);
        Task<Paiement?> GetByCommandeIdAsync(int commandeId);
        Task<List<Paiement>> GetAllAsync();
        Task<Paiement> AddAsync(Paiement paiement);
    }
}
