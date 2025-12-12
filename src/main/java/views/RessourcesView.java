package views;

import java.util.List;

import entity.Burger;
import entity.Complement;
import entity.Menu;
import entity.Zone;
import services.IRessourceInitializerService;
import services.impl.RessourceInitializerService;

public class RessourcesView {
    private final IRessourceInitializerService ressourceService = new RessourceInitializerService();

    public void afficher() {
        System.out.println("\n╔════════════════════════════════════════════════╗");
        System.out.println("║     BRASIL BURGER - CRÉATION DES RESSOURCES   ║");
        System.out.println("╚════════════════════════════════════════════════╝\n");
        
        // Initialiser toutes les ressources
        ressourceService.initialiserRessources();
        
        System.out.println("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        
        // Afficher les ressources créées
        afficherBurgers();
        afficherComplements();
        afficherMenus();
        afficherZones();
        
        System.out.println("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        System.out.println("✓ Toutes les ressources ont été créées avec succès !");
    }
    
    private void afficherBurgers() {
        System.out.println("\n🍔 BURGERS DISPONIBLES");
        System.out.println("─────────────────────────────────────────────────");
        List<Burger> burgers = ressourceService.creerBurgers();
        for (Burger burger : burgers) {
            System.out.printf("  [ID: %d] %-20s %.0f FCFA%n", 
                burger.getId(), burger.getNom(), burger.getPrix());
        }
    }
    
    private void afficherComplements() {
        System.out.println("\n🥤 COMPLÉMENTS DISPONIBLES");
        System.out.println("─────────────────────────────────────────────────");
        List<Complement> complements = ressourceService.creerComplements();
        
        System.out.println("  Boissons:");
        for (int i = 0; i < 4; i++) {
            Complement comp = complements.get(i);
            System.out.printf("    [ID: %d] %-20s %.0f FCFA%n", 
                comp.getId(), comp.getNom(), comp.getPrix());
        }
        
        System.out.println("  Frites:");
        for (int i = 4; i < complements.size(); i++) {
            Complement comp = complements.get(i);
            System.out.printf("    [ID: %d] %-20s %.0f FCFA%n", 
                comp.getId(), comp.getNom(), comp.getPrix());
        }
    }
    
    private void afficherMenus() {
        System.out.println("\n📦 MENUS DISPONIBLES");
        System.out.println("─────────────────────────────────────────────────");
        List<Burger> burgers = ressourceService.creerBurgers();
        List<Complement> complements = ressourceService.creerComplements();
        List<Menu> menus = ressourceService.creerMenus(burgers, complements);
        
        for (Menu menu : menus) {
            System.out.printf("  [ID: %d] %-20s %.0f FCFA%n", 
                menu.getId(), menu.getNom(), menu.getPrix());
            System.out.printf("    └─ %s + %s + %s%n",
                menu.getBurger().getNom(),
                menu.getBoisson().getNom(),
                menu.getFrites().getNom());
        }
    }
    
    private void afficherZones() {
        System.out.println("\n🚚 ZONES DE LIVRAISON");
        System.out.println("─────────────────────────────────────────────────");
        List<Zone> zones = ressourceService.creerZones();
        for (Zone zone : zones) {
            System.out.printf("  [ID: %d] %-15s Livraison: %.0f FCFA%n", 
                zone.getId(), zone.getNom(), zone.getPrixLivraison());
            System.out.println("    └─ Quartiers: " + String.join(", ", zone.getQuartiers()));
        }
    }
}
