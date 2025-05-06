package exercise_5;

import java.sql.*;
import java.util.Scanner;

public class Solution {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        int villainId = Integer.parseInt(scanner.nextLine());

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            String villainName = getVillainName(connection, villainId);
            if (villainName == null) {
                System.out.println("No such villain was found");
            } else {
                // To execute the following code within a transaction, disable the auto-commit behavior.
                connection.setAutoCommit(false);

                try {
                    int releasedMinionsCount = releaseMinions(connection, villainId);
                    deleteVillain(connection, villainId);

                    connection.commit();
                    System.out.printf("%s was deleted%n", villainName);
                    System.out.printf("%d minions released%n", releasedMinionsCount);
                } catch (SQLException e) {
                    connection.rollback();
                    throw e;
                }
            }

        }
    }

    private static String getVillainName(Connection connection, int villainId) throws SQLException {
        try (PreparedStatement statement = connection.prepareStatement("select v.name from villains as v where v.id = ?");) {
            statement.setInt(1, villainId);

            try (ResultSet result = statement.executeQuery()) {
                if (result.next()) return result.getString("name");
                else return null;
            }
        }
    }

    private static int releaseMinions(Connection connection, int villainId) throws SQLException {
        try (PreparedStatement statement = connection.prepareStatement("delete from minions_villains as mv where mv.villain_id = ?")) {
            statement.setInt(1, villainId);
            return statement.executeUpdate();
        }
    }

    private static void deleteVillain(Connection connection, int villainId) throws SQLException {
        try (PreparedStatement statement = connection.prepareStatement("delete from villains as v where v.id = ?")) {
            statement.setInt(1, villainId);
            statement.executeUpdate();
        }
    }
}
