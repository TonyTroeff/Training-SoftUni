package exercise_3;

import java.sql.*;
import java.util.Scanner;

public class Solution {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);

        String[] minionData = scanner.nextLine().split("\\s+");
        String minionName = minionData[1];
        int minionAge = Integer.parseInt(minionData[2]);
        String town = minionData[3];

        String[] villainData = scanner.nextLine().split("\\s+");
        String villainName = villainData[1];

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            int townId = ensureTown(connection, town);
            int villainId = ensureVillain(connection, villainName);
            int minionId = createMinion(connection, minionName, minionAge, townId);
            connectMinionAndVillain(connection, minionId, villainId);

            System.out.printf("Successfully added %s to be minion of %s.%n", minionName, villainName);
        }
    }

    private static int ensureTown(Connection connection, String town) throws SQLException {
        try (PreparedStatement selectStatement = connection.prepareStatement("select t.id from towns as t where t.name = ?")) {
            selectStatement.setString(1, town);

            try (ResultSet result = selectStatement.executeQuery()) {
                if (result.next()) return result.getInt("id");
            }
        }

        try (PreparedStatement insertStatement = connection.prepareStatement("insert into towns (name, country) values (?, NULL)", Statement.RETURN_GENERATED_KEYS)) {
            insertStatement.setString(1, town);
            insertStatement.executeUpdate();

            System.out.printf("Town %s was added to the database.%n", town);
            return getFirstGeneratedKey(insertStatement);
        }
    }

    private static int ensureVillain(Connection connection, String villainName) throws SQLException {
        try (PreparedStatement selectStatement = connection.prepareStatement("select v.id from villains as v where v.name = ?")) {
            selectStatement.setString(1, villainName);

            try (ResultSet result = selectStatement.executeQuery()) {
                if (result.next()) return result.getInt("id");
            }
        }

        try (PreparedStatement insertStatement = connection.prepareStatement("insert into villains (name, evilness_factor) values (?, 'evil')", Statement.RETURN_GENERATED_KEYS)) {
            insertStatement.setString(1, villainName);
            insertStatement.executeUpdate();

            System.out.printf("Villain %s was added to the database.%n", villainName);

            return getFirstGeneratedKey(insertStatement);
        }
    }

    private static int createMinion(Connection connection, String minionName, int minionAge, int townId) throws SQLException {
        try (PreparedStatement insertStatement = connection.prepareStatement("insert into minions (name, age, town_id) values (?, ?, ?)", Statement.RETURN_GENERATED_KEYS)) {
            insertStatement.setString(1, minionName);
            insertStatement.setInt(2, minionAge);
            insertStatement.setInt(3, townId);
            insertStatement.executeUpdate();

            return getFirstGeneratedKey(insertStatement);
        }
    }

    private static void connectMinionAndVillain(Connection connection, int minionId, int villainId) throws SQLException {
        try (PreparedStatement insertStatement = connection.prepareStatement("insert into minions_villains (minion_id, villain_id) values (?, ?)")) {
            insertStatement.setInt(1, minionId);
            insertStatement.setInt(2, villainId);

            insertStatement.executeUpdate();
        }
    }

    private static int getFirstGeneratedKey(Statement insertStatement) throws SQLException {
        try (ResultSet generatedKeys = insertStatement.getGeneratedKeys()) {
            generatedKeys.next(); // We can use `generatedKeys.first()` here as well.
            return generatedKeys.getInt(1);
        }
    }
}
