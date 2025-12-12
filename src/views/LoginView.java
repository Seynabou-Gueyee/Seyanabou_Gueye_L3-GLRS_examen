package views;

import entity.Utilisateur;
import service.IUtilisateurService;
import service.impl.UtilisateurService;

import java.util.Optional;
import java.util.Scanner;

public class LoginView {
    private final IUtilisateurService userService = new UtilisateurService();
    private final Scanner scanner = new Scanner(System.in);

    public void afficher() {
        System.out.println("=== BRASIL BURGER ADMIN ===");
        System.out.print("Email: ");
        String email = scanner.nextLine();
        System.out.print("Mot de passe: ");
        String pwd = scanner.nextLine();

        Optional<Utilisateur> user = userService.seConnecter(email, pwd);

        if (user.isPresent()) {
            System.out.println("Connexion réussie ! Bienvenue " + user.get().getPrenom());
        } else {
            System.out.println("Erreur d'identifiants.");
        }
    }
}
