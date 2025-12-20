using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Services.impl
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients
                .Include(c => c.Commandes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client?> AuthenticateAsync(string email, string password)
        {
            var client = await GetByEmailAsync(email);
            if (client == null)
                return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, client.MotDePasse);
            return isPasswordValid ? client : null;
        }

        public async Task<Client> CreateAsync(Client client)
        {
            // Hash du mot de passe
            client.MotDePasse = BCrypt.Net.BCrypt.HashPassword(client.MotDePasse);
            
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<int> GetTotalCommandesAsync(int clientId)
        {
            return await _context.Commandes
                .Where(c => c.ClientId == clientId)
                .CountAsync();
        }

        public async Task<decimal> GetTotalDepensesAsync(int clientId)
        {
            var commandes = await _context.Commandes
                .Include(c => c.CommandeItems)
                .Include(c => c.Zone)
                .Where(c => c.ClientId == clientId && c.Etat != EtatCommande.Annulee)
                .ToListAsync();

            return commandes.Sum(c => c.MontantTotal);
        }

        public async Task<int> GetTotalLivraisonsAsync(int clientId)
        {
            return await _context.Commandes
                .Where(c => c.ClientId == clientId && c.TypeService == TypeService.Livraison)
                .CountAsync();
        }
    }
}
