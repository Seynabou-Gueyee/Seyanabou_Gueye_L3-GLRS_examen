using System.ComponentModel.DataAnnotations;

namespace brasilBurger.Models
{
    public class Zone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public decimal PrixLivraison { get; set; }

        [StringLength(500)]
        public string? Quartiers { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        // Relations
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}
