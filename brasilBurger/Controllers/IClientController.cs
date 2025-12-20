using Microsoft.AspNetCore.Mvc;
using brasilBurger.Models;

namespace brasilBurger.Controllers
{
    public interface IClientController
    {
        // Catalogue
        Task<IActionResult> Catalogue(string? filtre);
        Task<IActionResult> DetailsBurger(int id);
        Task<IActionResult> DetailsMenu(int id);
        
        // Panier
        IActionResult Panier();
        Task<IActionResult> AjouterAuPanier(string type, int id, int quantite);
        IActionResult ModifierQuantite(string type, int id, int quantite);
        IActionResult SupprimerDuPanier(string type, int id);
        IActionResult ViderPanier();
        
        // Commandes
        Task<IActionResult> MesCommandes();
        Task<IActionResult> Commander();
        Task<IActionResult> ValiderCommande(TypeService typeService, int? zoneId, ModePaiement modePaiement);
        Task<IActionResult> AnnulerCommande(int id);
        
        // Profil
        Task<IActionResult> Profil();
        Task<IActionResult> ModifierProfil(string nom, string prenom, string telephone);
        Task<IActionResult> ChangerMotDePasse(string ancienMotDePasse, string nouveauMotDePasse);
    }
}
