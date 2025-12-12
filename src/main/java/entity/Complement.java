package entity;

public class Complement {
    private int id;
    private String nom;
    private String image;
    private double prix;
    private boolean archive;

    public Complement() {}

    public Complement(String nom, String image, double prix) {
        this.nom = nom;
        this.image = image;
        this.prix = prix;
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

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public double getPrix() {
        return prix;
    }

    public void setPrix(double prix) {
        this.prix = prix;
    }

    public boolean isArchive() {
        return archive;
    }

    public void setArchive(boolean archive) {
        this.archive = archive;
    }
}
