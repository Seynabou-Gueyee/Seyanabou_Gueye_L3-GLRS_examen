using System.ComponentModel.DataAnnotations;

namespace brasilBurger.Models
{
    public class CommandeItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CommandeId { get; set; }
        public Commande Commande { get; set; } = null!;

        public int? BurgerId { get; set; }
        public Burger? Burger { get; set; }

        public int? MenuId { get; set; }
        public Menu? Menu { get; set; }

        public int? ComplementId { get; set; }
        public Complement? Complement { get; set; }

        [Required]
        public int Quantite { get; set; } = 1;

        [Required]
        public decimal PrixUnitaire { get; set; }
    }
}
