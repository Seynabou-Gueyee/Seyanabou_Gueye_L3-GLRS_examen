using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Repositories.impl
{
    public class BurgerRepository : IBurgerRepository
    {
        private readonly ApplicationDbContext _context;

        public BurgerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Burger?> GetByIdAsync(int id)
        {
            return await _context.Burgers.FindAsync(id);
        }

        public async Task<List<Burger>> GetAllAsync()
        {
            return await _context.Burgers.ToListAsync();
        }

        public async Task<List<Burger>> GetActifsAsync()
        {
            return await _context.Burgers
                .Where(b => !b.EstArchive)
                .ToListAsync();
        }

        public async Task<Burger> AddAsync(Burger burger)
        {
            _context.Burgers.Add(burger);
            await _context.SaveChangesAsync();
            return burger;
        }

        public async Task<Burger> UpdateAsync(Burger burger)
        {
            _context.Entry(burger).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return burger;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var burger = await _context.Burgers.FindAsync(id);
            if (burger == null)
                return false;

            _context.Burgers.Remove(burger);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
