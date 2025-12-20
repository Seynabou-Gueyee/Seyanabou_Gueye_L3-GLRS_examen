using Microsoft.AspNetCore.Mvc;
using brasilBurger.Models;
using brasilBurger.Services;
using brasilBurger.Repositories;
using brasilBurger.ViewModels;
using System.Text.Json;

namespace brasilBurger.Controllers.impl
{
    [Route("Client")]
    public class ClientController : Controller, IClientController
    {
        private readonly IBurgerService _burgerService;
        private readonly IMenuService _menuService;
        private readonly IComplementService _complementService;
        private readonly ICommandeService _commandeService;
        private readonly IClientService _clientService;
        private readonly IPaiementService _paiementService;
        private readonly IZoneRepository _zoneRepository;

        public ClientController(
            IBurgerService burgerService,
            IMenuService menuService,
            IComplementService complementService,
            ICommandeService commandeService,
            IClientService clientService,
            IPaiementService paiementService,
            IZoneRepository zoneRepository)
        {
            _burgerService = burgerService;
            _menuService = menuService;
            _complementService = complementService;
            _commandeService = commandeService;
            _clientService = clientService;
            _paiementService = paiementService;
            _zoneRepository = zoneRepository;
        }

        private int? GetClientId() => HttpContext.Session.GetInt32("ClientId");
        
        private List<PanierItem> GetPanier()
        {
            var panierJson = HttpContext.Session.GetString("Panier");
            if (string.IsNullOrEmpty(panierJson))
                return new List<PanierItem>();
            return JsonSerializer.Deserialize<List<PanierItem>>(panierJson) ?? new List<PanierItem>();
        }

        private void SavePanier(List<PanierItem> panier)
        {
            var panierJson = JsonSerializer.Serialize(panier);
            HttpContext.Session.SetString("Panier", panierJson);
        }

        #region Catalogue

        [HttpGet]
        [Route("Catalogue")]
        public async Task<IActionResult> Catalogue(string? filtre)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");

            var viewModel = new brasilBurger.ViewModels.CatalogueViewModel
            {
                Burgers = await _burgerService.GetActifsAsync(),
                Menus = await _menuService.GetActifsAsync(),
                Complements = await _complementService.GetActifsAsync(),
                Filter = filtre ?? "all"
            };

            return View("~/Views/Client/Catalogue.cshtml", viewModel);
        }

        [HttpGet]
        [Route("DetailsBurger/{id}")]
        public async Task<IActionResult> DetailsBurger(int id)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var burger = await _burgerService.GetByIdAsync(id);
            if (burger == null)
                return NotFound();

            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");
            ViewBag.Complements = await _complementService.GetActifsAsync();

            return View("~/Views/Client/DetailsBurger.cshtml", burger);
        }

        [HttpGet]
        [Route("DetailsMenu/{id}")]
        public async Task<IActionResult> DetailsMenu(int id)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var menu = await _menuService.GetByIdAsync(id);
            if (menu == null)
                return NotFound();

            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");

            return View("~/Views/Client/DetailsMenu.cshtml", menu);
        }

        #endregion

        #region Panier

        [HttpGet]
        [Route("Panier")]
        public IActionResult Panier()
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");
            var panier = GetPanier();
            
            // Debug
            Console.WriteLine($"=== PAGE PANIER ===");
            Console.WriteLine($"Nombre d'articles: {panier.Count}");
            
            var viewModel = new PanierViewModel
            {
                Items = panier,
                Total = panier.Sum(p => p.Prix * p.Quantite),
                FraisLivraison = 0,
                NombreArticles = panier.Sum(p => p.Quantite)
            };

            return View("~/Views/Client/Panier.cshtml", viewModel);
        }

        [HttpGet]
        [Route("TestPanier")]
        public IActionResult TestPanier()
        {
            var panier = GetPanier();
            var sessionId = HttpContext.Session.Id;
            var clientId = GetClientId();
            
            return Content($@"
                Session ID: {sessionId}
                Client ID: {clientId}
                Nombre articles: {panier.Count}
                Articles: {string.Join(", ", panier.Select(p => $"{p.Nom} x{p.Quantite}"))}
            ");
        }

        [HttpPost]
        [Route("AjouterAuPanier")]
        public async Task<IActionResult> AjouterAuPanier(string type, int id, int quantite = 1)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var panier = GetPanier();
            PanierItem? newItem = null;

            switch (type.ToLower())
            {
                case "burger":
                    var burger = await _burgerService.GetByIdAsync(id);
                    if (burger != null)
                    {
                        newItem = new PanierItem
                        {
                            Type = "Burger",
                            Id = burger.Id,
                            Nom = burger.Nom,
                            Prix = burger.Prix,
                            ImageUrl = burger.ImageUrl,
                            Quantite = quantite
                        };
                    }
                    break;

                case "menu":
                    var menu = await _menuService.GetByIdAsync(id);
                    if (menu != null)
                    {
                        newItem = new PanierItem
                        {
                            Type = "Menu",
                            Id = menu.Id,
                            Nom = menu.Nom,
                            Prix = menu.PrixTotal,
                            ImageUrl = menu.ImageUrl,
                            Quantite = quantite
                        };
                    }
                    break;

                case "complement":
                    var complement = await _complementService.GetByIdAsync(id);
                    if (complement != null)
                    {
                        newItem = new PanierItem
                        {
                            Type = "Complement",
                            Id = complement.Id,
                            Nom = complement.Nom,
                            Prix = complement.Prix,
                            ImageUrl = complement.ImageUrl,
                            Quantite = quantite
                        };
                    }
                    break;
            }

            if (newItem != null)
            {
                var existingItem = panier.FirstOrDefault(p => p.Type == newItem.Type && p.Id == newItem.Id);
                if (existingItem != null)
                {
                    existingItem.Quantite += quantite;
                    TempData["Success"] = $"{newItem.Nom} : quantité mise à jour ({existingItem.Quantite})";
                }
                else
                {
                    panier.Add(newItem);
                    TempData["Success"] = $"{newItem.Nom} ajouté au panier !";
                }

                SavePanier(panier);
                
                // Debug
                Console.WriteLine($"=== PANIER APRÈS AJOUT ===");
                Console.WriteLine($"Nombre d'articles: {panier.Count}");
                foreach (var item in panier)
                {
                    Console.WriteLine($"- {item.Nom} x{item.Quantite}");
                }
            }
            else
            {
                TempData["Error"] = "Impossible d'ajouter cet article au panier";
            }

            return RedirectToAction("Catalogue");
        }

        [HttpPost]
        [Route("ModifierQuantite")]
        public IActionResult ModifierQuantite(string type, int id, int quantite)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var panier = GetPanier();
            var item = panier.FirstOrDefault(p => p.Type == type && p.Id == id);

            if (item != null)
            {
                if (quantite <= 0)
                {
                    panier.Remove(item);
                }
                else
                {
                    item.Quantite = quantite;
                }

                SavePanier(panier);
            }

            return RedirectToAction("Panier");
        }

        [HttpPost]
        [Route("SupprimerDuPanier")]
        public IActionResult SupprimerDuPanier(string type, int id)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var panier = GetPanier();
            var item = panier.FirstOrDefault(p => p.Type == type && p.Id == id);

            if (item != null)
            {
                panier.Remove(item);
                SavePanier(panier);
            }

            return RedirectToAction("Panier");
        }

        [HttpPost]
        [Route("ViderPanier")]
        public IActionResult ViderPanier()
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            HttpContext.Session.Remove("Panier");
            return RedirectToAction("Panier");
        }

        #endregion

        #region Commandes

        [HttpGet]
        [Route("MesCommandes")]
        public async Task<IActionResult> MesCommandes()
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");
            var commandes = await _commandeService.GetByClientIdAsync(clientId.Value);

            return View("~/Views/Client/MesCommandes.cshtml", commandes);
        }

        [HttpGet]
        [Route("Commander")]
        public async Task<IActionResult> Commander()
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var panier = GetPanier();
            if (!panier.Any())
                return RedirectToAction("Panier");

            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");
            ViewBag.Panier = panier;
            ViewBag.MontantArticles = panier.Sum(p => p.Prix * p.Quantite);
            ViewBag.Zones = await _zoneRepository.GetAllAsync();

            return View("~/Views/Client/Commande.cshtml");
        }

        [HttpGet]
        [Route("Paiement")]
        public IActionResult Paiement()
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var panier = GetPanier();
            
            // Debug: Log le contenu du panier
            Console.WriteLine($"=== DEBUG PAIEMENT ===");
            Console.WriteLine($"Session ID: {HttpContext.Session.Id}");
            Console.WriteLine($"Nombre d'articles dans le panier: {panier.Count}");
            foreach (var item in panier)
            {
                Console.WriteLine($"- {item.Nom} x{item.Quantite} = {item.Prix * item.Quantite} FCFA");
            }
            
            // TEMPORAIRE : ne pas rediriger, afficher la page même si vide
            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");

            var viewModel = new PanierViewModel
            {
                Items = panier,
                Total = panier.Sum(p => p.Prix * p.Quantite),
                FraisLivraison = 0,
                NombreArticles = panier.Sum(p => p.Quantite)
            };

            Console.WriteLine($"ViewModel Total: {viewModel.Total}");
            Console.WriteLine($"ViewModel Items Count: {viewModel.Items.Count}");
            
            return View("~/Views/Client/Paiement.cshtml", viewModel);
        }

        [HttpPost]
        [Route("ValiderCommande")]
        public async Task<IActionResult> ValiderCommande(TypeService typeService, int? zoneId, ModePaiement modePaiement)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var panier = GetPanier();
            if (!panier.Any())
                return RedirectToAction("Panier");

            // Créer la commande
            var commande = new Commande
            {
                ClientId = clientId.Value,
                TypeService = typeService,
                Etat = EtatCommande.EnAttente,
                ZoneId = typeService == TypeService.Livraison ? zoneId : null,
                NumeroCommande = $"CMD-{DateTime.Now:yyyyMMddHHmmss}",
                CommandeItems = new List<CommandeItem>()
            };

            // Ajouter les items
            foreach (var item in panier)
            {
                var commandeItem = new CommandeItem
                {
                    Quantite = item.Quantite,
                    PrixUnitaire = item.Prix
                };

                switch (item.Type.ToLower())
                {
                    case "burger":
                        commandeItem.BurgerId = item.Id;
                        break;
                    case "menu":
                        commandeItem.MenuId = item.Id;
                        break;
                    case "complement":
                        commandeItem.ComplementId = item.Id;
                        break;
                }

                commande.CommandeItems.Add(commandeItem);
            }

            await _commandeService.CreateAsync(commande);

            // Créer le paiement
            var paiement = new Paiement
            {
                CommandeId = commande.Id,
                Montant = commande.MontantTotal,
                ModePaiement = modePaiement,
                Reference = $"PAY-{DateTime.Now:yyyyMMddHHmmss}"
            };

            await _paiementService.CreateAsync(paiement);

            // Vider le panier
            HttpContext.Session.Remove("Panier");

            TempData["Success"] = "Votre commande a été passée avec succès !";
            return RedirectToAction("MesCommandes");
        }

        [HttpPost]
        [Route("AnnulerCommande")]
        public async Task<IActionResult> AnnulerCommande(int id)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var commande = await _commandeService.GetByIdAsync(id);
            if (commande == null || commande.ClientId != clientId.Value)
                return NotFound();

            if (commande.Etat == EtatCommande.EnAttente)
            {
                await _commandeService.AnnulerAsync(id);
                TempData["Success"] = "La commande a été annulée";
            }

            return RedirectToAction("MesCommandes");
        }

        #endregion

        #region Profil

        [HttpGet]
        [Route("Profil")]
        public async Task<IActionResult> Profil()
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var client = await _clientService.GetByIdAsync(clientId.Value);
            if (client == null)
                return NotFound();

            ViewBag.ClientNom = HttpContext.Session.GetString("ClientNom");
            ViewBag.TotalCommandes = await _clientService.GetTotalCommandesAsync(clientId.Value);
            ViewBag.TotalDepenses = await _clientService.GetTotalDepensesAsync(clientId.Value);
            ViewBag.TotalLivraisons = await _clientService.GetTotalLivraisonsAsync(clientId.Value);

            return View("~/Views/Client/Profil.cshtml", client);
        }

        [HttpPost]
        [Route("ModifierProfil")]
        public async Task<IActionResult> ModifierProfil(string nom, string prenom, string telephone)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var client = await _clientService.GetByIdAsync(clientId.Value);
            if (client == null)
                return NotFound();

            client.Nom = nom;
            client.Prenom = prenom;
            client.Telephone = telephone;

            await _clientService.UpdateAsync(client);

            HttpContext.Session.SetString("ClientNom", $"{client.Prenom} {client.Nom}");
            TempData["Success"] = "Profil modifié avec succès";

            return RedirectToAction("Profil");
        }

        [HttpPost]
        [Route("ChangerMotDePasse")]
        public async Task<IActionResult> ChangerMotDePasse(string ancienMotDePasse, string nouveauMotDePasse)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Account");

            var client = await _clientService.GetByIdAsync(clientId.Value);
            if (client == null)
                return NotFound();

            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(ancienMotDePasse, client.MotDePasse);
            if (!isOldPasswordValid)
            {
                TempData["Error"] = "L'ancien mot de passe est incorrect";
                return RedirectToAction("Profil");
            }

            client.MotDePasse = BCrypt.Net.BCrypt.HashPassword(nouveauMotDePasse);
            await _clientService.UpdateAsync(client);

            TempData["Success"] = "Mot de passe modifié avec succès";
            return RedirectToAction("Profil");
        }

        #endregion
    }
}
