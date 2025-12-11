package repository;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class DBConnection {
    private static final String URL = "jdbc:postgresql://ep-id-projet.region.aws.neon.tech:5432/brasil-burger?sslmode=require";
    private static final String USER = "votre_user";
    private static final String PASSWORD = "votre_password";

    public static Connection getConnection() throws SQLException {
        return DriverManager.getConnection(URL, USER, PASSWORD);
    }
    
}
