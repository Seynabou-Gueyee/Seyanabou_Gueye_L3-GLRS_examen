package repository;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class DBConnection {
    private static final String URL = "psql 'postgresql://neondb_owner:npg_MJzu8tRAgN2L@ep-round-wind-agof96rx-pooler.c-2.eu-central-1.aws.neon.tech/neondb?sslmode=require&channel_binding=require'";
    private static final String USER = "neondb_owner";
    private static final String PASSWORD = "npg_MJzu8tRAgN2L";

    public static Connection getConnection() throws SQLException {
        return DriverManager.getConnection(URL, USER, PASSWORD);
    }
    
}
