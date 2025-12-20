using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace brasilBurger.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public bool EstArchive { get; set; } = false;

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        // Relations
        [Required]
        public int BurgerId { get; set; }
        public Burger Burger { get; set; } = null!;

        public ICollection<MenuComplement> MenuComplements { get; set; } = new List<MenuComplement>();
        public ICollection<CommandeItem> CommandeItems { get; set; } = new List<CommandeItem>();

        // Prix calculé
        [NotMapped]
        public decimal PrixTotal => (Burger?.Prix ?? 0) + MenuComplements.Sum(mc => mc.Complement?.Prix ?? 0);
    }
}
