import java.util.Optional;
package repository;

public interface IUtilisateurRepository extends IGenericRepository<Utilisateur> {
    Optional<Utilisateur> findByEmail(String email);
}