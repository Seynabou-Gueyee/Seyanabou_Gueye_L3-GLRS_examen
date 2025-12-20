using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Repositories.impl
{
    public class ComplementRepository : IComplementRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Complement?> GetByIdAsync(int id)
        {
            return await _context.Complements.FindAsync(id);
        }

        public async Task<List<Complement>> GetAllAsync()
        {
            return await _context.Complements.ToListAsync();
        }

        public async Task<List<Complement>> GetActifsAsync()
        {
            return await _context.Complements
                .Where(c => !c.EstArchive)
                .ToListAsync();
        }

        public async Task<List<Complement>> GetByTypeAsync(TypeComplement type)
        {
            return await _context.Complements
                .Where(c => c.Type == type && !c.EstArchive)
                .ToListAsync();
        }

        public async Task<Complement> AddAsync(Complement complement)
        {
            _context.Complements.Add(complement);
            await _context.SaveChangesAsync();
            return complement;
        }

        public async Task<Complement> UpdateAsync(Complement complement)
        {
            _context.Entry(complement).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return complement;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var complement = await _context.Complements.FindAsync(id);
            if (complement == null)
                return false;

            _context.Complements.Remove(complement);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
