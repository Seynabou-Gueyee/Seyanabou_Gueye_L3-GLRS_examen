package repository.impl;

import entity.*;
import repository.DBConnection;
import repository.IUtilisateurRepository;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
public class UtilisateurRepository implements IUtilisateurRepository{
    @Override
    public Utilisateur save(Utilisateur user) {
        String sql = "INSERT INTO UTILISATEUR (nom, prenom, telephone, email, mot_de_passe, role) VALUES (?, ?, ?, ?, ?, ?)";
        
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            
            stmt.setString(1, user.getNom());
            stmt.setString(2, user.getPrenom());
            stmt.setString(3, "000000000");
            stmt.setString(4, user.getEmail());
            stmt.setString(5, user.getMotDePasse());
            stmt.setObject(6, user.getRole().toString(), java.sql.Types.OTHER);

            int affectedRows = stmt.executeUpdate();
            if (affectedRows > 0) {
                try (ResultSet rs = stmt.getGeneratedKeys()) {
                    if (rs.next()) {
                        return user;
                    }
                }
            }
        } catch (SQLException e) { e.printStackTrace(); }
        return null;
    }

    @Override
    public Optional<Utilisateur> findByEmail(String email) {
        String sql = "SELECT * FROM UTILISATEUR WHERE email = ?";
        try (Connection conn = DBConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            
            stmt.setString(1, email);
            ResultSet rs = stmt.executeQuery();
            
            if (rs.next()) {
                RoleType role = RoleType.valueOf(rs.getString("role"));
                Utilisateur user;
                
                if (role == RoleType.GESTIONNAIRE) {
                    user = new Gestionnaire(
                        rs.getInt("id_utilisateur"),
                        rs.getString("nom"),
                        rs.getString("prenom"),
                        rs.getString("telephone"),
                        rs.getString("email"),
                        rs.getString("mot_de_passe")
                    );
                } else {
                    user = new Livreur(
                        rs.getInt("id_utilisateur"),
                        rs.getString("nom"),
                        rs.getString("prenom"),
                        rs.getString("telephone"),
                        rs.getString("email"),
                        rs.getString("mot_de_passe")
                    );
                }
                return Optional.of(user);
            }
        } catch (SQLException e) { e.printStackTrace(); }
        return Optional.empty();
    }

    @Override
    public List<Utilisateur> findAll() {
        return new ArrayList<>();
    }
}
