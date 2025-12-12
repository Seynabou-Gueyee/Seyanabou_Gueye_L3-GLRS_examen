package entity;


public class Gestionnaire extends Utilisateur{
    public Gestionnaire(int id, String nom, String prenom, String telephone, String email, String motDePasse) {
        super(id, nom, prenom, telephone, email, motDePasse, RoleType.GESTIONNAIRE);
    }
    
    public Gestionnaire(String nom, String prenom, String telephone, String email, String motDePasse) {
        super(0, nom, prenom, telephone, email, motDePasse, RoleType.GESTIONNAIRE);
    }

    @Override
    public String getDetails() { return "Administrateur Système"; }
}
