namespace brasilBurger.Models
{
    public class PanierItem
    {
        public string Type { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public string? ImageUrl { get; set; }
        
        public decimal SousTotal => Prix * Quantite;
    }
}
