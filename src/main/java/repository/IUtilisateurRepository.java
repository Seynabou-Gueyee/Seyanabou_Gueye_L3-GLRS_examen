package repository;

import java.util.Optional;

import entity.Utilisateur;

public interface IUtilisateurRepository extends IGenericRepository<Utilisateur> {
    Optional<Utilisateur> findByEmail(String email);
}