package orm;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;

public class MyConnector {
    private final Connection connection;

    public MyConnector(String databaseServerUrl, String username, String password, String databaseName) throws SQLException {
        Properties properties = new Properties();
        properties.setProperty("user", username);
        properties.setProperty("password", password);

        String connectionString = String.format("jdbc:%s/%s", databaseServerUrl, databaseName);
        this.connection = DriverManager.getConnection(connectionString, properties);
    }

    public Connection getConnection() {
        return this.connection;
    }
}
