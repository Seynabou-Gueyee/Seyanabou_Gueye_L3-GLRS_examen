using System.ComponentModel.DataAnnotations;

namespace brasilBurger.Models
{
    public enum ModePaiement
    {
        Wave,
        OrangeMoney
    }

    public class Paiement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CommandeId { get; set; }
        public Commande Commande { get; set; } = null!;

        [Required]
        public decimal Montant { get; set; }

        [Required]
        public ModePaiement ModePaiement { get; set; }

        public DateTime DatePaiement { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string? Reference { get; set; }
    }
}
