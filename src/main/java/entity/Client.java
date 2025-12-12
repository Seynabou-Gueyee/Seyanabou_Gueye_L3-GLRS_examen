package entity;

public class Client extends Utilisateur {
    
    public Client(int id, String nom, String prenom, String telephone, String email, String motDePasse) {
        super(id, nom, prenom, telephone, email, motDePasse, RoleType.CLIENT);
    }

    @Override
    public String getDetails() {
        return "Client: " + nom + " " + prenom + " (" + email + ")";
    }
}
