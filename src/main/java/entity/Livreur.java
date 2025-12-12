package entity;

public class Livreur extends Utilisateur {
    public Livreur(int id, String nom, String prenom, String telephone, String email, String motDePasse) {
        super(id, nom, prenom, telephone, email, motDePasse, RoleType.LIVREUR);
    }

    public Livreur(String nom, String prenom, String telephone, String email, String motDePasse) {
        super(0, nom, prenom, telephone, email, motDePasse, RoleType.LIVREUR);
    }

    @Override
    public String getDetails() { return "Personnel de Livraison"; }
    
}
