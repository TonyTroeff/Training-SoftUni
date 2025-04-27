import java.sql.*;
import java.util.Scanner;

public class Lab1 {
    public static void main(String[] args) throws SQLException {
        Scanner scanner = new Scanner(System.in);
        double minSalary = Double.parseDouble(scanner.nextLine());

        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/soft_uni", "root", "root")) {
            try (PreparedStatement statement = connection.prepareStatement("select first_name, last_name from employees where salary > ?")) {
                statement.setDouble(1, minSalary);

                try (ResultSet result = statement.executeQuery()) {
                    while (result.next()) {
                        String firstName = result.getString("first_name");
                        String lastName = result.getString("last_name");
                        System.out.printf("%s %s%n", firstName, lastName);
                    }
                }
            }
        }
    }
}