package entity;

public class Menu {
    private int id;
    private String nom;
    private String image;
    private Burger burger;
    private Complement boisson;
    private Complement frites;
    private boolean archive;

    public Menu() {}

    public Menu(String nom, String image, Burger burger, Complement boisson, Complement frites) {
        this.nom = nom;
        this.image = image;
        this.burger = burger;
        this.boisson = boisson;
        this.frites = frites;
        this.archive = false;
    }

    // Calcul du prix du menu (somme des composants)
    public double getPrix() {
        double total = 0;
        if (burger != null) total += burger.getPrix();
        if (boisson != null) total += boisson.getPrix();
        if (frites != null) total += frites.getPrix();
        return total;
    }

    // Getters et Setters
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getNom() {
        return nom;
    }

    public void setNom(String nom) {
        this.nom = nom;
    }

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public Burger getBurger() {
        return burger;
    }

    public void setBurger(Burger burger) {
        this.burger = burger;
    }

    public Complement getBoisson() {
        return boisson;
    }

    public void setBoisson(Complement boisson) {
        this.boisson = boisson;
    }

    public Complement getFrites() {
        return frites;
    }

    public void setFrites(Complement frites) {
        this.frites = frites;
    }

    public boolean isArchive() {
        return archive;
    }

    public void setArchive(boolean archive) {
        this.archive = archive;
    }
}
