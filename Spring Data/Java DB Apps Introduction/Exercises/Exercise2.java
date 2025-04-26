import java.sql.*;
import java.util.Scanner;

public class Exercise2 {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        int id = Integer.parseInt(scanner.nextLine());

        Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root");
        PreparedStatement villainStatement = connection.prepareStatement("select v.name from villains as v where v.id = ?");
        villainStatement.setInt(1, id);

        ResultSet villainResult = villainStatement.executeQuery();
        if (!villainResult.next()) {
            System.out.printf("No villain with ID %d exists in the database.%n", id);
        } else {
            String villainName = villainResult.getString("name");
            System.out.printf("Villain: %s%n", villainName);

            PreparedStatement minionsStatement = connection.prepareStatement("select m.name, m.age from minions as m join minions_db.minions_villains mv on m.id = mv.minion_id where mv.villain_id = ?");
            minionsStatement.setInt(1, id);

            ResultSet minionsResult = minionsStatement.executeQuery();
            int sequenceId = 0;
            while (minionsResult.next()) {
                String minionName = minionsResult.getString("name");
                int minionAge = minionsResult.getInt("age");

                System.out.printf("%d. %s %d%n", ++sequenceId, minionName, minionAge);
            }
        }
    }
}
