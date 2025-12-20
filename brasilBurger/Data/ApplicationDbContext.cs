using Microsoft.EntityFrameworkCore;
using brasilBurger.Models;

namespace brasilBurger.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Complement> Complements { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuComplement> MenuComplements { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CommandeItem> CommandeItems { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Livreur> Livreurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration MenuComplement (table de liaison)
            modelBuilder.Entity<MenuComplement>()
                .HasKey(mc => new { mc.MenuId, mc.ComplementId });

            modelBuilder.Entity<MenuComplement>()
                .HasOne(mc => mc.Menu)
                .WithMany(m => m.MenuComplements)
                .HasForeignKey(mc => mc.MenuId);

            modelBuilder.Entity<MenuComplement>()
                .HasOne(mc => mc.Complement)
                .WithMany(c => c.MenuComplements)
                .HasForeignKey(mc => mc.ComplementId);

            // Configuration Paiement (1 à 1 avec Commande)
            modelBuilder.Entity<Paiement>()
                .HasOne(p => p.Commande)
                .WithOne(c => c.Paiement)
                .HasForeignKey<Paiement>(p => p.CommandeId);

            // Précision pour les décimaux
            modelBuilder.Entity<Burger>()
                .Property(b => b.Prix)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Complement>()
                .Property(c => c.Prix)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Zone>()
                .Property(z => z.PrixLivraison)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CommandeItem>()
                .Property(ci => ci.PrixUnitaire)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Paiement>()
                .Property(p => p.Montant)
                .HasPrecision(10, 2);
        }
    }
}
