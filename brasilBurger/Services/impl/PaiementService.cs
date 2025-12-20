using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Services.impl
{
    public class PaiementService : IPaiementService
    {
        private readonly ApplicationDbContext _context;

        public PaiementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Paiement?> GetByCommandeIdAsync(int commandeId)
        {
            return await _context.Paiements
                .FirstOrDefaultAsync(p => p.CommandeId == commandeId);
        }

        public async Task<Paiement> CreateAsync(Paiement paiement)
        {
            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();
            return paiement;
        }
    }
}
