package entity;

import java.util.List;

public class Zone {
    private int id;
    private String nom;
    private List<String> quartiers;
    private double prixLivraison;

    public Zone() {}

    public Zone(String nom, List<String> quartiers, double prixLivraison) {
        this.nom = nom;
        this.quartiers = quartiers;
        this.prixLivraison = prixLivraison;
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

    public List<String> getQuartiers() {
        return quartiers;
    }

    public void setQuartiers(List<String> quartiers) {
        this.quartiers = quartiers;
    }

    public double getPrixLivraison() {
        return prixLivraison;
    }

    public void setPrixLivraison(double prixLivraison) {
        this.prixLivraison = prixLivraison;
    }
}
