package orm;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class MyConnector {
    private final Connection connection;
    private final String databaseName;

    public MyConnector(String databaseServerUrl, String user, String password, String databaseName) throws SQLException {
        String connectionString = String.format("jdbc:%s/%s", databaseServerUrl, databaseName);
        this.connection = DriverManager.getConnection(connectionString, user, password);
        this.databaseName = databaseName;
    }

    public Connection getConnection() {
        return this.connection;
    }

    public String getDatabaseName() {
        return this.databaseName;
    }
}
