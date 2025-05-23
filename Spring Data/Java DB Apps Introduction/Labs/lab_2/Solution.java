package lab_2;

import java.sql.*;
import java.util.Scanner;

public class Solution {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        String username = scanner.nextLine();

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/diablo", "root", "root")) {
            try (PreparedStatement statement = connection.prepareStatement("select first_name, last_name, count(*) as games_count from users as u join diablo.users_games ug on u.id = ug.user_id where u.user_name = ? group by u.id")) {
                statement.setString(1, username);

                try (ResultSet result = statement.executeQuery()) {
                    if (result.next()) {
                        String firstName = result.getString("first_name");
                        String lastName = result.getString("last_name");
                        int gamesCount = result.getInt("games_count");

                        System.out.printf("User: %s%n", username);
                        System.out.printf("%s %s has played %d games%n", firstName, lastName, gamesCount);
                    } else {
                        System.out.println("No such user exists");
                    }
                }
            }
        }
    }
}
