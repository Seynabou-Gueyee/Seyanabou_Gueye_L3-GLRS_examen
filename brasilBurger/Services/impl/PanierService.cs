using Microsoft.EntityFrameworkCore;
using brasilBurger.Data;
using brasilBurger.Models;
using System.Text.Json;

namespace brasilBurger.Services.impl
{
    public class PanierService : IPanierService
    {
        private readonly ApplicationDbContext _context;
        private const string PANIER_SESSION_KEY = "Panier_";

        public PanierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PanierItem>> GetPanierAsync(int clientId)
        {
            // Récupération du panier depuis la session ou la base de données
            // Pour simplifier, on utilise une liste en mémoire
            var panierItems = new List<PanierItem>();
            
            // TODO: Implémenter la logique de récupération depuis la session
            // ou utiliser une table temporaire dans la base de données
            
            return await Task.FromResult(panierItems);
        }

        public async Task<bool> AddToPanierAsync(int clientId, string type, int itemId, int quantite = 1)
        {
            try
            {
                var panier = await GetPanierAsync(clientId);
                PanierItem? item = null;

                switch (type.ToLower())
                {
                    case "burger":
                        var burger = await _context.Burgers.FindAsync(itemId);
                        if (burger != null && !burger.EstArchive)
                        {
                            item = new PanierItem
                            {
                                Type = "Burger",
                                Id = burger.Id,
                                Nom = burger.Nom,
                                Prix = burger.Prix,
                                Quantite = quantite,
                                ImageUrl = burger.ImageUrl
                            };
                        }
                        break;

                    case "menu":
                        var menu = await _context.Menus
                            .Include(m => m.Burger)
                            .Include(m => m.MenuComplements)
                                .ThenInclude(mc => mc.Complement)
                            .FirstOrDefaultAsync(m => m.Id == itemId);
                        if (menu != null && !menu.EstArchive)
                        {
                            item = new PanierItem
                            {
                                Type = "Menu",
                                Id = menu.Id,
                                Nom = menu.Nom,
                                Prix = menu.PrixTotal,
                                Quantite = quantite,
                                ImageUrl = menu.ImageUrl
                            };
                        }
                        break;

                    case "complement":
                        var complement = await _context.Complements.FindAsync(itemId);
                        if (complement != null && !complement.EstArchive)
                        {
                            item = new PanierItem
                            {
                                Type = "Complement",
                                Id = complement.Id,
                                Nom = complement.Nom,
                                Prix = complement.Prix,
                                Quantite = quantite,
                                ImageUrl = complement.ImageUrl
                            };
                        }
                        break;
                }

                if (item != null)
                {
                    var existingItem = panier.FirstOrDefault(p => p.Type == item.Type && p.Id == item.Id);
                    if (existingItem != null)
                    {
                        existingItem.Quantite += quantite;
                    }
                    else
                    {
                        panier.Add(item);
                    }

                    // TODO: Sauvegarder le panier en session ou en base de données
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateQuantiteAsync(int clientId, string type, int itemId, int quantite)
        {
            var panier = await GetPanierAsync(clientId);
            var item = panier.FirstOrDefault(p => p.Type == type && p.Id == itemId);

            if (item != null)
            {
                if (quantite <= 0)
                {
                    return await RemoveFromPanierAsync(clientId, type, itemId);
                }

                item.Quantite = quantite;
                // TODO: Sauvegarder le panier
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveFromPanierAsync(int clientId, string type, int itemId)
        {
            var panier = await GetPanierAsync(clientId);
            var item = panier.FirstOrDefault(p => p.Type == type && p.Id == itemId);

            if (item != null)
            {
                panier.Remove(item);
                // TODO: Sauvegarder le panier
                return true;
            }

            return false;
        }

        public async Task<bool> ClearPanierAsync(int clientId)
        {
            // TODO: Supprimer le panier de la session ou de la base de données
            return await Task.FromResult(true);
        }

        public async Task<decimal> GetPanierTotalAsync(int clientId)
        {
            var panier = await GetPanierAsync(clientId);
            return panier.Sum(p => p.Prix * p.Quantite);
        }

        public async Task<int> GetPanierCountAsync(int clientId)
        {
            var panier = await GetPanierAsync(clientId);
            return panier.Sum(p => p.Quantite);
        }
    }
}
