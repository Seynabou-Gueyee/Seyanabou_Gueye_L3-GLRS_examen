using System.ComponentModel.DataAnnotations;

namespace brasilBurger.Models
{
    public enum TypeComplement
    {
        Frite,
        Boisson
    }

    public class Complement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public decimal Prix { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public TypeComplement Type { get; set; }

        public bool EstArchive { get; set; } = false;

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        // Relations
        public ICollection<CommandeItem> CommandeItems { get; set; } = new List<CommandeItem>();
        public ICollection<MenuComplement> MenuComplements { get; set; } = new List<MenuComplement>();
    }
}
