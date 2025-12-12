package services.impl;

import java.util.Optional;

import entity.Gestionnaire;
import entity.Utilisateur;
import repository.IRepositoryFactory;
import repository.IUtilisateurRepository;
import repository.impl.RepositoryFactory;
import services.IUtilisateurService;

public class UtilisateurService implements IUtilisateurService {
    private final IRepositoryFactory factory = RepositoryFactory.getInstance();
    private final IUtilisateurRepository userRepo = factory.getUtilisateurRepository();

    @Override
    public Optional<Utilisateur> seConnecter(String email, String password) {
        Optional<Utilisateur> userOpt = userRepo.findByEmail(email);
        
        if (userOpt.isPresent()) {
            Utilisateur user = userOpt.get();
            if (user.getMotDePasse().equals(password)) {
                return Optional.of(user);
            }
        }
        return Optional.empty();
    }

    @Override
    public Utilisateur creerGestionnaire(String nom, String prenom, String tel, String email, String pwd) {
        Gestionnaire g = new Gestionnaire(nom, prenom, tel, email, pwd);
        return userRepo.save(g);
    }
}
