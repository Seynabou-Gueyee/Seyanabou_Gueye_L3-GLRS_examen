using brasilBurger.Models;
using System.ComponentModel.DataAnnotations;

namespace brasilBurger.ViewModels
{
    public class CommanderViewModel
    {
        public List<PanierItem> PanierItems { get; set; } = new List<PanierItem>();
        public Client? Client { get; set; }
        public List<Zone> Zones { get; set; } = new List<Zone>();
        
        [Required(ErrorMessage = "Le type de service est requis")]
        public TypeService TypeService { get; set; }
        
        public int? ZoneId { get; set; }
        public List<Zone> ZonesDisponibles { get; set; } = new List<Zone>();
        
        public decimal MontantArticles { get; set; }
        public decimal SousTotal => PanierItems.Sum(i => i.SousTotal);
        public decimal FraisLivraison { get; set; }
        public decimal MontantTotal { get; set; }
        public int NombreArticles => PanierItems.Sum(i => i.Quantite);
        
        [Required(ErrorMessage = "Le mode de paiement est requis")]
        public ModePaiement ModePaiement { get; set; }
    }

    public class MesCommandesViewModel
    {
        public List<Commande> CommandesEnCours { get; set; } = new List<Commande>();
        public List<Commande> CommandesTerminees { get; set; } = new List<Commande>();
    }

    public class CommandeDetailViewModel
    {
        public Commande Commande { get; set; } = null!;
        public Paiement? Paiement { get; set; }
        public Zone? Zone { get; set; }
    }
}
