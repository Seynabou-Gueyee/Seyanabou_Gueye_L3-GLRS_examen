package services;
import entity.Utilisateur;;
import java.util.Optional;

public class IUtilisateurService {
    Optional<Utilisateur> seConnecter(String email, String password);
    Utilisateur creerGestionnaire(String nom, String prenom, String tel, String email, String pwd);
}
