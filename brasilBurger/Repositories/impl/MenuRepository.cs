using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Repositories.impl
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                    .ThenInclude(mc => mc.Complement)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            return await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                    .ThenInclude(mc => mc.Complement)
                .ToListAsync();
        }

        public async Task<List<Menu>> GetActifsAsync()
        {
            return await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                    .ThenInclude(mc => mc.Complement)
                .Where(m => !m.EstArchive)
                .ToListAsync();
        }

        public async Task<Menu> AddAsync(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> UpdateAsync(Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
                return false;

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
