using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Services.impl
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> AuthenticateAsync(string email, string password)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == email);

            if (client == null)
                return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, client.MotDePasse);
            return isPasswordValid ? client : null;
        }

        public async Task<Client> RegisterAsync(Client client)
        {
            // Hash du mot de passe
            client.MotDePasse = BCrypt.Net.BCrypt.HashPassword(client.MotDePasse);
            client.DateInscription = DateTime.UtcNow;

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Clients.AnyAsync(c => c.Email == email);
        }

        public async Task<Client?> GetCurrentClientAsync(int clientId)
        {
            return await _context.Clients
                .Include(c => c.Commandes)
                .FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<bool> ChangePasswordAsync(int clientId, string oldPassword, string newPassword)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
                return false;

            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(oldPassword, client.MotDePasse);
            if (!isOldPasswordValid)
                return false;

            client.MotDePasse = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
