using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace brasilBurger.Models
{
    public enum TypeService
    {
        SurPlace,
        AEmporter,
        Livraison
    }

    public enum EtatCommande
    {
        EnAttente,
        EnPreparation,
        Terminee,
        Annulee,
        Validee
    }

    public class Commande
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NumeroCommande { get; set; } = string.Empty;

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        [Required]
        public TypeService TypeService { get; set; }

        [Required]
        public EtatCommande Etat { get; set; } = EtatCommande.EnAttente;

        public int? ZoneId { get; set; }
        public Zone? Zone { get; set; }

        public int? LivreurId { get; set; }
        public Livreur? Livreur { get; set; }

        public DateTime DateCommande { get; set; } = DateTime.UtcNow;

        public DateTime? DateLivraison { get; set; }

        // Relations
        public ICollection<CommandeItem> CommandeItems { get; set; } = new List<CommandeItem>();
        public Paiement? Paiement { get; set; }

        // Montant calculé
        [NotMapped]
        public decimal MontantTotal
        {
            get
            {
                var montant = CommandeItems.Sum(ci => ci.PrixUnitaire * ci.Quantite);
                if (TypeService == TypeService.Livraison && Zone != null)
                {
                    montant += Zone.PrixLivraison;
                }
                return montant;
            }
        }
    }
}
