using brasilBurger.Models;

namespace brasilBurger.ViewModels
{
    public class PanierViewModel
    {
        public List<PanierItem> Items { get; set; } = new List<PanierItem>();
        public decimal Total { get; set; }
        public decimal SousTotal => Items.Sum(i => i.SousTotal);
        public decimal FraisLivraison { get; set; }
        public int NombreArticles { get; set; }
        public bool EstVide => !Items.Any();
    }

    public class AjouterAuPanierViewModel
    {
        public string Type { get; set; } = string.Empty; // "burger", "menu", "complement"
        public int ItemId { get; set; }
        public int Quantite { get; set; } = 1;
        public List<int>? ComplementIds { get; set; }
    }

    public class ModifierQuantiteViewModel
    {
        public string Type { get; set; } = string.Empty;
        public int ItemId { get; set; }
        public int Quantite { get; set; }
    }
}
