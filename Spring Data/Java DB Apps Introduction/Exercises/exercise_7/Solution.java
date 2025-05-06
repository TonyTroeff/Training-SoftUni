package exercise_7;

import java.sql.*;
import java.util.Arrays;
import java.util.Collections;
import java.util.Scanner;

public class Solution {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        int[] minionIds = Arrays.stream(scanner.nextLine().split("\\s+")).mapToInt(Integer::parseInt).toArray();

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            updateMinions(connection, minionIds);
            printAllMinions(connection);
        }
    }

    private static void updateMinions(Connection connection, int[] minionIds) throws SQLException {
        String idPlaceholders = String.join(", ", Collections.nCopies(minionIds.length, "?"));
        String updateQuery = String.format("update minions as m set m.age = m.age + 1, m.name = lower(m.name) where m.id in (%s)", idPlaceholders);

        try (PreparedStatement statement = connection.prepareStatement(updateQuery)) {
            for (int i = 0; i < minionIds.length; i++) statement.setInt(i + 1, minionIds[i]);
            statement.executeUpdate();
        }
    }

    private static void printAllMinions(Connection connection) throws SQLException {
        try (PreparedStatement statement = connection.prepareStatement("select m.name, m.age from minions as m")) {
            try (ResultSet result = statement.executeQuery()) {
                while (result.next()) {
                    String name = result.getString("name");
                    int age = result.getInt("age");

                    System.out.printf("%s %d%n", name, age);
                }
            }
        }
    }
}
