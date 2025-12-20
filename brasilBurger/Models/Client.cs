using System.ComponentModel.DataAnnotations;

namespace brasilBurger.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string MotDePasse { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Adresse { get; set; }

        public string? PhotoUrl { get; set; }

        public DateTime DateInscription { get; set; } = DateTime.UtcNow;

        // Relations
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}
