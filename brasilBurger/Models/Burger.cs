using System.ComponentModel.DataAnnotations;

namespace brasilBurger.Models
{
    public class Burger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Prix { get; set; }

        public string? ImageUrl { get; set; }

        public bool EstArchive { get; set; } = false;

        public bool EstBestSeller { get; set; } = false;

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        // Relations
        public ICollection<CommandeItem> CommandeItems { get; set; } = new List<CommandeItem>();
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
    }
}
