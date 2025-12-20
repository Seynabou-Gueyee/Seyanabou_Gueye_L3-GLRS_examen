using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Services.impl
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Menus
                .Include(m => m.Burger)
                .Include(m => m.MenuComplements)
                    .ThenInclude(mc => mc.Complement)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Menu> CreateAsync(Menu menu)
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

        public async Task<bool> ArchiverAsync(int id)
        {
            var menu = await GetByIdAsync(id);
            if (menu == null)
                return false;

            menu.EstArchive = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
