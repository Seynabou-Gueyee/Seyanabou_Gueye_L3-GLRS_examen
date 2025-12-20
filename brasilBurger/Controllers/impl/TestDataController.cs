using Microsoft.AspNetCore.Mvc;
using brasilBurger.Data;
using brasilBurger.Models;

namespace brasilBurger.Controllers.impl
{
    [Route("TestData")]
    public class TestDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Count")]
        public IActionResult CountData()
        {
            var burgersCount = _context.Burgers.Count();
            var menusCount = _context.Menus.Count();
            var complementsCount = _context.Complements.Count();
            var zonesCount = _context.Zones.Count();
            
            return Ok($"Burgers: {burgersCount}, Menus: {menusCount}, Compléments: {complementsCount}, Zones: {zonesCount}");
        }

        [HttpGet]
        [Route("Seed")]
        public async Task<IActionResult> SeedData()
        {
            // Vérifier si des données existent déjà
            if (_context.Burgers.Any())
            {
                return Ok("Les données de test existent déjà");
            }

            // Créer des burgers de test
            var burgers = new List<Burger>
            {
                new Burger 
                { 
                    Nom = "Brasília Burger", 
                    Description = "Burger emblématique avec bœuf brésilien, fromage coalho, oignons caramélisés et sauce chimichurri", 
                    Prix = 3500,
                    ImageUrl = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=400",
                    EstArchive = false
                },
                new Burger 
                { 
                    Nom = "Copacabana Delight", 
                    Description = "Double steak juteux, bacon fumé, cheddar fondant, tomate fraîche et mayonnaise à l'ail", 
                    Prix = 4000,
                    ImageUrl = "https://images.unsplash.com/photo-1550547660-d9450f859349?w=400",
                    EstArchive = false
                },
                new Burger 
                { 
                    Nom = "Amazônia Spicy", 
                    Description = "Burger épicé au poulet grillé, piments jalapeños, guacamole et salsa piquante", 
                    Prix = 3200,
                    ImageUrl = "https://images.unsplash.com/photo-1572802419224-296b0aeee0d9?w=400",
                    EstArchive = false
                },
                new Burger 
                { 
                    Nom = "Rio Classic", 
                    Description = "Le classique revisité : bœuf angus, laitue croquante, tomate, cornichons et sauce maison", 
                    Prix = 2800,
                    ImageUrl = "https://images.unsplash.com/photo-1561758033-d89a9ad46330?w=400",
                    EstArchive = false
                },
                new Burger 
                { 
                    Nom = "Salvador Cheese", 
                    Description = "Triple fromage (cheddar, mozzarella, coalho), oignons frits croustillants et sauce barbecue", 
                    Prix = 3800,
                    ImageUrl = "https://images.unsplash.com/photo-1553979459-d2229ba7433b?w=400",
                    EstArchive = false
                },
                new Burger 
                { 
                    Nom = "Bahia Beach", 
                    Description = "Burger de poisson frais pané, salade fraîche, avocat et sauce tartare citronnée", 
                    Prix = 3600,
                    ImageUrl = "https://images.unsplash.com/photo-1585238341710-4a8e07d8a4e4?w=400",
                    EstArchive = false
                }
            };

            _context.Burgers.AddRange(burgers);
            await _context.SaveChangesAsync();

            // Créer des compléments de test
            var complements = new List<Complement>
            {
                new Complement { Nom = "Frites Classiques", Prix = 1000, Type = TypeComplement.Frite, EstArchive = false },
                new Complement { Nom = "Frites de Patate Douce", Prix = 1200, Type = TypeComplement.Frite, EstArchive = false },
                new Complement { Nom = "Onion Rings", Prix = 1200, Type = TypeComplement.Frite, EstArchive = false },
                new Complement { Nom = "Coca-Cola 33cl", Prix = 800, Type = TypeComplement.Boisson, EstArchive = false },
                new Complement { Nom = "Sprite 33cl", Prix = 800, Type = TypeComplement.Boisson, EstArchive = false },
                new Complement { Nom = "Fanta 33cl", Prix = 800, Type = TypeComplement.Boisson, EstArchive = false },
                new Complement { Nom = "Guaraná Brasil", Prix = 1000, Type = TypeComplement.Boisson, EstArchive = false },
                new Complement { Nom = "Eau Minérale", Prix = 500, Type = TypeComplement.Boisson, EstArchive = false }
            };

            _context.Complements.AddRange(complements);
            await _context.SaveChangesAsync();

            // Créer des menus de test (liés aux burgers)
            var menus = new List<Menu>
            {
                new Menu 
                { 
                    Nom = "Menu Brasil",
                    BurgerId = burgers[0].Id,
                    EstArchive = false
                },
                new Menu 
                { 
                    Nom = "Menu Copacabana",
                    BurgerId = burgers[1].Id,
                    EstArchive = false
                },
                new Menu 
                { 
                    Nom = "Menu Famille",
                    BurgerId = burgers[2].Id,
                    EstArchive = false
                }
            };

            _context.Menus.AddRange(menus);
            await _context.SaveChangesAsync();

            // Lier les compléments aux menus
            var menuComplements = new List<MenuComplement>
            {
                // Menu Brasil: Frites + Coca
                new MenuComplement { MenuId = menus[0].Id, ComplementId = complements[0].Id },
                new MenuComplement { MenuId = menus[0].Id, ComplementId = complements[3].Id },
                
                // Menu Copacabana: Frites Patate + Guarana
                new MenuComplement { MenuId = menus[1].Id, ComplementId = complements[1].Id },
                new MenuComplement { MenuId = menus[1].Id, ComplementId = complements[6].Id },
                
                // Menu Famille: Frites + Onion Rings + 3 Boissons
                new MenuComplement { MenuId = menus[2].Id, ComplementId = complements[0].Id },
                new MenuComplement { MenuId = menus[2].Id, ComplementId = complements[2].Id },
                new MenuComplement { MenuId = menus[2].Id, ComplementId = complements[3].Id },
                new MenuComplement { MenuId = menus[2].Id, ComplementId = complements[4].Id },
                new MenuComplement { MenuId = menus[2].Id, ComplementId = complements[5].Id }
            };

            _context.MenuComplements.AddRange(menuComplements);
            await _context.SaveChangesAsync();

            // Créer des zones de test
            var zones = new List<Zone>
            {
                new Zone { Nom = "Centre-ville", PrixLivraison = 1000 },
                new Zone { Nom = "Plateau", PrixLivraison = 1500 },
                new Zone { Nom = "Cocody", PrixLivraison = 2000 },
                new Zone { Nom = "Yopougon", PrixLivraison = 2500 },
                new Zone { Nom = "Abobo", PrixLivraison = 3000 }
            };

            _context.Zones.AddRange(zones);
            await _context.SaveChangesAsync();

            return Ok("Données de test créées avec succès ! " + 
                     $"{burgers.Count} burgers, {complements.Count} compléments, {menus.Count} menus et {zones.Count} zones.");
        }

        [HttpGet]
        [Route("Clear")]
        public async Task<IActionResult> ClearData()
        {
            _context.Paiements.RemoveRange(_context.Paiements);
            _context.CommandeItems.RemoveRange(_context.CommandeItems);
            _context.Commandes.RemoveRange(_context.Commandes);
            _context.MenuComplements.RemoveRange(_context.MenuComplements);
            _context.Menus.RemoveRange(_context.Menus);
            _context.Complements.RemoveRange(_context.Complements);
            _context.Burgers.RemoveRange(_context.Burgers);
            _context.Zones.RemoveRange(_context.Zones);
            _context.Clients.RemoveRange(_context.Clients);

            await _context.SaveChangesAsync();

            return Ok("Toutes les données ont été supprimées");
        }
    }
}
