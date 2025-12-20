using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Repositories.impl
{
    public class PaiementRepository : IPaiementRepository
    {
        private readonly ApplicationDbContext _context;

        public PaiementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Paiement?> GetByIdAsync(int id)
        {
            return await _context.Paiements
                .Include(p => p.Commande)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paiement?> GetByCommandeIdAsync(int commandeId)
        {
            return await _context.Paiements
                .FirstOrDefaultAsync(p => p.CommandeId == commandeId);
        }

        public async Task<List<Paiement>> GetAllAsync()
        {
            return await _context.Paiements
                .Include(p => p.Commande)
                .OrderByDescending(p => p.DatePaiement)
                .ToListAsync();
        }

        public async Task<Paiement> AddAsync(Paiement paiement)
        {
            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();
            return paiement;
        }
    }
}
