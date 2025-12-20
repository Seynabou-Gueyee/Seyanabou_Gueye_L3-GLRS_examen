using brasilBurger.Models;

namespace brasilBurger.Services
{
    public interface ICommandeService
    {
        Task<List<Commande>> GetAllAsync();
        Task<List<Commande>> GetByClientIdAsync(int clientId);
        Task<List<Commande>> GetByDateAsync(DateTime date);
        Task<List<Commande>> GetByEtatAsync(EtatCommande etat);
        Task<List<Commande>> GetEnCoursJourAsync();
        Task<List<Commande>> GetValideesJourAsync();
        Task<List<Commande>> GetAnnuleesJourAsync();
        Task<Commande?> GetByIdAsync(int id);
        Task<Commande?> GetByNumeroAsync(string numero);
        Task<Commande> CreateAsync(Commande commande);
        Task<Commande> UpdateAsync(Commande commande);
        Task<bool> AnnulerAsync(int id);
        Task<bool> ChangerEtatAsync(int id, EtatCommande etat);
        Task<decimal> GetRecettesJournalieresAsync();
        Task<List<(string Nom, int Quantite)>> GetProduitsLesPlusVendusJourAsync();
    }
}
