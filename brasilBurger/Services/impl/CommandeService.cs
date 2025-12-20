using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Services.impl
{
    public class CommandeService : ICommandeService
    {
        private readonly ApplicationDbContext _context;

        public CommandeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Commande>> GetAllAsync()
        {
            return await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.CommandeItems)
                .Include(c => c.Zone)
                .Include(c => c.Paiement)
                .OrderByDescending(c => c.DateCommande)
                .ToListAsync();
        }

        public async Task<List<Commande>> GetByClientIdAsync(int clientId)
        {
            return await _context.Commandes
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Burger)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Menu)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Complement)
                .Include(c => c.Zone)
                .Include(c => c.Paiement)
                .Where(c => c.ClientId == clientId)
                .OrderByDescending(c => c.DateCommande)
                .ToListAsync();
        }

        public async Task<List<Commande>> GetByDateAsync(DateTime date)
        {
            var dateOnly = date.Date;
            return await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.CommandeItems)
                .Where(c => c.DateCommande.Date == dateOnly)
                .OrderByDescending(c => c.DateCommande)
                .ToListAsync();
        }

        public async Task<List<Commande>> GetByEtatAsync(EtatCommande etat)
        {
            return await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.CommandeItems)
                .Where(c => c.Etat == etat)
                .OrderByDescending(c => c.DateCommande)
                .ToListAsync();
        }

        public async Task<List<Commande>> GetEnCoursJourAsync()
        {
            var today = DateTime.Today;
            return await _context.Commandes
                .Include(c => c.CommandeItems)
                .Where(c => c.DateCommande.Date == today && 
                           (c.Etat == EtatCommande.EnPreparation || c.Etat == EtatCommande.EnAttente))
                .ToListAsync();
        }

        public async Task<List<Commande>> GetValideesJourAsync()
        {
            var today = DateTime.Today;
            return await _context.Commandes
                .Include(c => c.CommandeItems)
                .Where(c => c.DateCommande.Date == today && c.Etat == EtatCommande.Validee)
                .ToListAsync();
        }

        public async Task<List<Commande>> GetAnnuleesJourAsync()
        {
            var today = DateTime.Today;
            return await _context.Commandes
                .Include(c => c.CommandeItems)
                .Where(c => c.DateCommande.Date == today && c.Etat == EtatCommande.Annulee)
                .ToListAsync();
        }

        public async Task<Commande?> GetByIdAsync(int id)
        {
            return await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Burger)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Menu)
                        .ThenInclude(m => m!.Burger)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Menu)
                        .ThenInclude(m => m!.MenuComplements)
                            .ThenInclude(mc => mc.Complement)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Complement)
                .Include(c => c.Zone)
                .Include(c => c.Paiement)
                .Include(c => c.Livreur)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Commande?> GetByNumeroAsync(string numero)
        {
            return await _context.Commandes
                .Include(c => c.CommandeItems)
                .FirstOrDefaultAsync(c => c.NumeroCommande == numero);
        }

        public async Task<Commande> CreateAsync(Commande commande)
        {
            // Générer numéro de commande
            commande.NumeroCommande = $"o{new Random().Next(1, 10000)}";
            
            _context.Commandes.Add(commande);
            await _context.SaveChangesAsync();
            return commande;
        }

        public async Task<Commande> UpdateAsync(Commande commande)
        {
            _context.Entry(commande).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return commande;
        }

        public async Task<bool> AnnulerAsync(int id)
        {
            var commande = await GetByIdAsync(id);
            if (commande == null)
                return false;

            commande.Etat = EtatCommande.Annulee;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangerEtatAsync(int id, EtatCommande etat)
        {
            var commande = await GetByIdAsync(id);
            if (commande == null)
                return false;

            commande.Etat = etat;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetRecettesJournalieresAsync()
        {
            var today = DateTime.Today;
            var commandes = await _context.Commandes
                .Include(c => c.CommandeItems)
                .Include(c => c.Zone)
                .Where(c => c.DateCommande.Date == today && 
                           c.Etat != EtatCommande.Annulee &&
                           c.Paiement != null)
                .ToListAsync();

            return commandes.Sum(c => c.MontantTotal);
        }

        public async Task<List<(string Nom, int Quantite)>> GetProduitsLesPlusVendusJourAsync()
        {
            var today = DateTime.Today;
            
            var burgers = await _context.CommandeItems
                .Include(ci => ci.Burger)
                .Include(ci => ci.Commande)
                .Where(ci => ci.Commande.DateCommande.Date == today && 
                            ci.Commande.Etat != EtatCommande.Annulee &&
                            ci.BurgerId != null)
                .GroupBy(ci => ci.Burger!.Nom)
                .Select(g => new { Nom = g.Key, Quantite = g.Sum(ci => ci.Quantite) })
                .OrderByDescending(x => x.Quantite)
                .Take(5)
                .ToListAsync();

            var menus = await _context.CommandeItems
                .Include(ci => ci.Menu)
                .Include(ci => ci.Commande)
                .Where(ci => ci.Commande.DateCommande.Date == today && 
                            ci.Commande.Etat != EtatCommande.Annulee &&
                            ci.MenuId != null)
                .GroupBy(ci => ci.Menu!.Nom)
                .Select(g => new { Nom = g.Key, Quantite = g.Sum(ci => ci.Quantite) })
                .OrderByDescending(x => x.Quantite)
                .Take(5)
                .ToListAsync();

            var combined = burgers.Concat(menus)
                .OrderByDescending(x => x.Quantite)
                .Take(5)
                .Select(x => (x.Nom, x.Quantite))
                .ToList();

            return combined;
        }
    }
}
