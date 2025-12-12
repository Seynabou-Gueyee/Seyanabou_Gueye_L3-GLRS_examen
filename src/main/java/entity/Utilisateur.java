package entity;

public abstract class Utilisateur {
    protected int id;
    protected String nom;
    protected String prenom;
    protected String email;
    protected String telephone;
    protected String motDePasse;
    protected RoleType role;

    public Utilisateur(int id, String nom, String prenom, String telephone, String email, String motDePasse, RoleType role) {
        this.id = id;
        this.nom = nom;
        this.prenom = prenom;
        this.telephone = telephone;
        this.email = email;
       	this.motDePasse = motDePasse;
        this.role = role;
    }

    public int getId() { return id; }
    public String getNom() { return nom; }
    public String getPrenom() { return prenom; }
    public String getEmail() { return email; }
    public String getMotDePasse() { return motDePasse; }
    public RoleType getRole() { return role; }
    
    public abstract String getDetails();
}
