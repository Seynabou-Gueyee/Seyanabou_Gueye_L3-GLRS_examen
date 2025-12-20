using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface IPaiementService
    {
        Task<Paiement?> GetByCommandeIdAsync(int commandeId);
        Task<Paiement> CreateAsync(Paiement paiement);
    }
}
