package exercise_8;

import java.sql.*;
import java.util.Scanner;

public class Solution {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        int minionId = Integer.parseInt(scanner.nextLine());

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            updateMinion(connection, minionId);
            printMinion(connection, minionId);
        }
    }

    private static void updateMinion(Connection connection, int minionId) throws SQLException {
        // The braces are not required for MySql.
        try (CallableStatement statement = connection.prepareCall("{call usp_get_older(?)}")) {
            statement.setInt(1, minionId);
            statement.execute();
        }
    }

    private static void printMinion(Connection connection, int minionId) throws SQLException {
        try (PreparedStatement statement = connection.prepareStatement("select m.name, m.age from minions as m where m.id = ?")) {
            statement.setInt(1, minionId);

            try (ResultSet result = statement.executeQuery()) {
                if (result.next()) {
                    String name = result.getString("name");
                    int age = result.getInt("age");

                    System.out.printf("%s %d%n", name, age);
                }
            }
        }
    }
}

/*
delimiter $$
create procedure usp_get_older(in minion_id int)
begin
    update minions as m set m.age = m.age + 1 where m.id = minion_id;
end $$
delimiter ;
*/