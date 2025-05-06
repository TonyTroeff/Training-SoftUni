package exercise_4;

import java.sql.*;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Scanner;

public class Solution {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        String country = scanner.nextLine();

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            // Choose one of the following options.
            solve1(connection, country);
            solve2(connection, country);
        }
    }

    // Select all towns and update them one by one while iterating the result set.
    private static void solve1(Connection connection, String country) throws SQLException {
        ArrayList<String> updatedNames = new ArrayList<>();

        // We need to set `ResultSet.CONCUR_UPDATABLE` so we can update the rows of the result set.
        try (PreparedStatement statement = connection.prepareStatement("select t.id, t.name from towns as t where t.country = ?", ResultSet.TYPE_FORWARD_ONLY, ResultSet.CONCUR_UPDATABLE)) {
            statement.setString(1, country);

            try (ResultSet result = statement.executeQuery()) {
                while (result.next()) {
                    String originalName = result.getString("name");
                    String newName = originalName.toUpperCase();

                    updatedNames.add(newName);
                    result.updateString("name", newName);
                    result.updateRow();
                }
            }
        }

        if (updatedNames.isEmpty()) System.out.println("No town names were affected.");
        else {
            System.out.printf("%d town names were affected.%n", updatedNames.size());
            System.out.printf("[%s]%n", String.join(", ", updatedNames));
        }
    }

    private static void solve2(Connection connection, String country) throws SQLException {
        ArrayList<Integer> idsToUpdate = getTownIds(connection, country);
        if (idsToUpdate.isEmpty()) {
            System.out.println("No town names were affected.");
            return;
        }

        String idPlaceholders = String.join(", ", Collections.nCopies(idsToUpdate.size(), "?"));
        String updateQuery = String.format("update towns as t set t.name = upper(t.name) where t.id in (%s)", idPlaceholders);
        try (PreparedStatement updateStatement = connection.prepareStatement(updateQuery)) {
            for (int i = 0; i < idsToUpdate.size(); i++) updateStatement.setInt(i + 1, idsToUpdate.get(i));
            updateStatement.executeUpdate();
        }
        System.out.printf("%d town names were affected.%n", idsToUpdate.size());

        String finalQuery = String.format("select t.name from towns as t where t.id in (%s)", idPlaceholders);
        ArrayList<String> updatedNames = new ArrayList<>(idsToUpdate.size());
        try (PreparedStatement finalStatement = connection.prepareStatement(finalQuery)) {
            for (int i = 0; i < idsToUpdate.size(); i++) finalStatement.setInt(i + 1, idsToUpdate.get(i));

            try (ResultSet finalResult = finalStatement.executeQuery()) {
                while (finalResult.next()) updatedNames.add(finalResult.getString("name"));
            }
        }

        System.out.printf("[%s]%n", String.join(", ", updatedNames));
    }

    private static ArrayList<Integer> getTownIds(Connection connection, String country) throws SQLException {
        ArrayList<Integer> idsToUpdate = new ArrayList<>();

        try (PreparedStatement initialStatement = connection.prepareStatement("select t.id from towns as t where t.country = ?")) {
            initialStatement.setString(1, country);

            try (ResultSet initialResult = initialStatement.executeQuery()) {
                while (initialResult.next()) idsToUpdate.add(initialResult.getInt("id"));
            }
        }

        return idsToUpdate;
    }
}
