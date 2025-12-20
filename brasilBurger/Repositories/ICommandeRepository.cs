using brasilBurger.Models;

namespace brasilBurger.Repositories
{
    public interface ICommandeRepository
    {
        Task<Commande?> GetByIdAsync(int id);
        Task<Commande?> GetByNumeroAsync(string numero);
        Task<List<Commande>> GetAllAsync();
        Task<List<Commande>> GetByClientIdAsync(int clientId);
        Task<List<Commande>> GetByDateAsync(DateTime date);
        Task<List<Commande>> GetByEtatAsync(EtatCommande etat);
        Task<Commande> AddAsync(Commande commande);
        Task<Commande> UpdateAsync(Commande commande);
        Task<bool> DeleteAsync(int id);
    }
}
