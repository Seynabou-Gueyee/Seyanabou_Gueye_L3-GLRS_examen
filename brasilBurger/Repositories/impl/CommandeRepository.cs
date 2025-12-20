using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Repositories.impl
{
    public class CommandeRepository : ICommandeRepository
    {
        private readonly ApplicationDbContext _context;

        public CommandeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Commande?> GetByIdAsync(int id)
        {
            return await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Burger)
                .Include(c => c.CommandeItems)
                    .ThenInclude(ci => ci.Menu)
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

        public async Task<List<Commande>> GetAllAsync()
        {
            return await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.CommandeItems)
                .Include(c => c.Zone)
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

        public async Task<Commande> AddAsync(Commande commande)
        {
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

        public async Task<bool> DeleteAsync(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);
            if (commande == null)
                return false;

            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
