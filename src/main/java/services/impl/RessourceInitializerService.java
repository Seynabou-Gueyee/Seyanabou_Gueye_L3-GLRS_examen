package services.impl;

import java.util.ArrayList;
import java.util.List;

import entity.Burger;
import entity.Complement;
import entity.Menu;
import entity.Zone;
import services.IRessourceInitializerService;

public class RessourceInitializerService implements IRessourceInitializerService {
    
    @Override
    public List<Burger> creerBurgers() {
        List<Burger> burgers = new ArrayList<>();
        
        Burger b1 = new Burger("Burger Classic", 2500, "classic.jpg");
        b1.setId(1);
        burgers.add(b1);
        
        Burger b2 = new Burger("Burger Cheese", 3000, "cheese.jpg");
        b2.setId(2);
        burgers.add(b2);
        
        Burger b3 = new Burger("Burger Chicken", 3500, "chicken.jpg");
        b3.setId(3);
        burgers.add(b3);
        
        Burger b4 = new Burger("Burger Deluxe", 4000, "deluxe.jpg");
        b4.setId(4);
        burgers.add(b4);
        
        Burger b5 = new Burger("Burger Veggie", 3200, "veggie.jpg");
        b5.setId(5);
        burgers.add(b5);
        
        return burgers;
    }
    
    @Override
    public List<Complement> creerComplements() {
        List<Complement> complements = new ArrayList<>();
        
        // Boissons
        Complement c1 = new Complement("Coca Cola", "coca.jpg", 500);
        c1.setId(1);
        complements.add(c1);
        
        Complement c2 = new Complement("Fanta", "fanta.jpg", 500);
        c2.setId(2);
        complements.add(c2);
        
        Complement c3 = new Complement("Sprite", "sprite.jpg", 500);
        c3.setId(3);
        complements.add(c3);
        
        Complement c4 = new Complement("Eau minérale", "eau.jpg", 300);
        c4.setId(4);
        complements.add(c4);
        
        // Frites
        Complement c5 = new Complement("Frites Small", "frites_s.jpg", 800);
        c5.setId(5);
        complements.add(c5);
        
        Complement c6 = new Complement("Frites Medium", "frites_m.jpg", 1000);
        c6.setId(6);
        complements.add(c6);
        
        Complement c7 = new Complement("Frites Large", "frites_l.jpg", 1200);
        c7.setId(7);
        complements.add(c7);
        
        return complements;
    }
    
    @Override
    public List<Menu> creerMenus(List<Burger> burgers, List<Complement> complements) {
        List<Menu> menus = new ArrayList<>();
        
        Complement coca = complements.get(0);
        Complement fanta = complements.get(1);
        Complement sprite = complements.get(2);
        Complement fritesM = complements.get(5);
        
        Menu m1 = new Menu("Menu Classic", "menu_classic.jpg", burgers.get(0), coca, fritesM);
        m1.setId(1);
        menus.add(m1);
        
        Menu m2 = new Menu("Menu Cheese", "menu_cheese.jpg", burgers.get(1), fanta, fritesM);
        m2.setId(2);
        menus.add(m2);
        
        Menu m3 = new Menu("Menu Chicken", "menu_chicken.jpg", burgers.get(2), sprite, fritesM);
        m3.setId(3);
        menus.add(m3);
        
        Menu m4 = new Menu("Menu Deluxe", "menu_deluxe.jpg", burgers.get(3), coca, fritesM);
        m4.setId(4);
        menus.add(m4);
        
        Menu m5 = new Menu("Menu Veggie", "menu_veggie.jpg", burgers.get(4), fanta, fritesM);
        m5.setId(5);
        menus.add(m5);
        
        return menus;
    }
    
    @Override
    public List<Zone> creerZones() {
        List<Zone> zones = new ArrayList<>();
        
        List<String> quartiersZone1 = new ArrayList<>();
        quartiersZone1.add("Plateau");
        quartiersZone1.add("Cocody");
        Zone z1 = new Zone("Zone 1", quartiersZone1, 1000);
        z1.setId(1);
        zones.add(z1);
        
        List<String> quartiersZone2 = new ArrayList<>();
        quartiersZone2.add("Yopougon");
        quartiersZone2.add("Abobo");
        Zone z2 = new Zone("Zone 2", quartiersZone2, 1500);
        z2.setId(2);
        zones.add(z2);
        
        List<String> quartiersZone3 = new ArrayList<>();
        quartiersZone3.add("Koumassi");
        quartiersZone3.add("Marcory");
        Zone z3 = new Zone("Zone 3", quartiersZone3, 1200);
        z3.setId(3);
        zones.add(z3);
        
        return zones;
    }
    
    @Override
    public void initialiserRessources() {
        System.out.println("=== Initialisation des Ressources ===");
        
        // Création des burgers
        List<Burger> burgers = creerBurgers();
        System.out.println(burgers.size() + " burgers créés");
        
        // Création des compléments
        List<Complement> complements = creerComplements();
        System.out.println(complements.size() + " compléments créés");
        
        // Création des menus
        List<Menu> menus = creerMenus(burgers, complements);
        System.out.println(menus.size() + " menus créés");
        
        // Création des zones
        List<Zone> zones = creerZones();
        System.out.println(zones.size() + " zones créées");
        
        System.out.println("=== Ressources initialisées avec succès ===\n");
    }
}
