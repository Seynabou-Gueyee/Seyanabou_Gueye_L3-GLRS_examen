using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Services.impl
{
    public class ComplementService : IComplementService
    {
        private readonly ApplicationDbContext _context;

        public ComplementService(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Complement?> GetByIdAsync(int id)
        {
            return await _context.Complements.FindAsync(id);
        }

        public async Task<Complement> CreateAsync(Complement complement)
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

        public async Task<bool> ArchiverAsync(int id)
        {
            var complement = await GetByIdAsync(id);
            if (complement == null)
                return false;

            complement.EstArchive = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
