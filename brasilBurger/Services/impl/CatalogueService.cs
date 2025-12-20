using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Services.impl
{
    public class CatalogueService : ICatalogueService
    {
        private readonly ApplicationDbContext _context;

        public CatalogueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Burger>> GetAllBurgersAsync()
        {
            return await _context.Burgers
                .Where(b => !b.EstArchive)
                .OrderBy(b => b.Nom)
                .ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                    .ThenInclude(mc => mc.Complement)
                .Where(m => !m.EstArchive)
                .OrderBy(m => m.Nom)
                .ToListAsync();
        }

        public async Task<IEnumerable<Complement>> GetAllComplementsAsync()
        {
            return await _context.Complements
                .Where(c => !c.EstArchive)
                .OrderBy(c => c.Nom)
                .ToListAsync();
        }

        public async Task<Burger?> GetBurgerByIdAsync(int id)
        {
            return await _context.Burgers
                .FirstOrDefaultAsync(b => b.Id == id && !b.EstArchive);
        }

        public async Task<Menu?> GetMenuByIdAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                    .ThenInclude(mc => mc.Complement)
                .FirstOrDefaultAsync(m => m.Id == id && !m.EstArchive);
        }

        public async Task<Complement?> GetComplementByIdAsync(int id)
        {
            return await _context.Complements
                .FirstOrDefaultAsync(c => c.Id == id && !c.EstArchive);
        }

        public async Task<IEnumerable<Burger>> SearchBurgersAsync(string searchTerm)
        {
            return await _context.Burgers
                .Where(b => !b.EstArchive && 
                       (b.Nom.Contains(searchTerm) || 
                        (b.Description != null && b.Description.Contains(searchTerm))))
                .OrderBy(b => b.Nom)
                .ToListAsync();
        }

        public async Task<IEnumerable<Menu>> SearchMenusAsync(string searchTerm)
        {
            return await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                    .ThenInclude(mc => mc.Complement)
                .Where(m => !m.EstArchive && m.Nom.Contains(searchTerm))
                .OrderBy(m => m.Nom)
                .ToListAsync();
        }
    }
}
