package services;
import java.util.Optional;

import entity.Utilisateur;

public interface IUtilisateurService {
    Optional<Utilisateur> seConnecter(String email, String password);
    Utilisateur creerGestionnaire(String nom, String prenom, String tel, String email, String pwd);
}
