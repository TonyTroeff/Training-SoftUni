import java.sql.*;
import java.util.Scanner;

public class Exercise2 {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        int villainId = Integer.parseInt(scanner.nextLine());

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            boolean villainExists = printVillainInfo(connection, villainId);
            if (villainExists) printMinionsInfo(connection, villainId);
        }
    }

    private static boolean printVillainInfo(Connection connection, int villainId) throws SQLException {
        try (PreparedStatement villainStatement = connection.prepareStatement("select v.name from villains as v where v.id = ?")) {
            villainStatement.setInt(1, villainId);

            try (ResultSet villainResult = villainStatement.executeQuery()) {
                boolean exists = villainResult.next();
                if (exists) {
                    String villainName = villainResult.getString("name");
                    System.out.printf("Villain: %s%n", villainName);
                } else {
                    System.out.printf("No villain with ID %d exists in the database.%n", villainId);
                }

                return exists;
            }
        }
    }

    private static void printMinionsInfo(Connection connection, int villainId) throws SQLException {
        try (PreparedStatement minionsStatement = connection.prepareStatement("select m.name, m.age from minions as m join minions_db.minions_villains mv on m.id = mv.minion_id where mv.villain_id = ?")) {
            minionsStatement.setInt(1, villainId);

            try (ResultSet minionsResult = minionsStatement.executeQuery()) {
                int sequenceId = 0;
                while (minionsResult.next()) {
                    String minionName = minionsResult.getString("name");
                    int minionAge = minionsResult.getInt("age");

                    System.out.printf("%d. %s %d%n", ++sequenceId, minionName, minionAge);
                }
            }
        }
    }
}
