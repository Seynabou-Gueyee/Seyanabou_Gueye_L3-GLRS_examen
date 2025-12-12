package entity;

public class Burger {
    private int id;
    private String nom;
    private double prix;
    private String image;
    private boolean archive;

    public Burger() {}

    public Burger(String nom, double prix, String image) {
        this.nom = nom;
        this.prix = prix;
        this.image = image;
        this.archive = false;
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

    public double getPrix() {
        return prix;
    }

    public void setPrix(double prix) {
        this.prix = prix;
    }

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public boolean isArchive() {
        return archive;
    }

    public void setArchive(boolean archive) {
        this.archive = archive;
    }
}
