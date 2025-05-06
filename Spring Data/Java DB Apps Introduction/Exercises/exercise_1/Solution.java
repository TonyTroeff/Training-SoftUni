package exercise_1;

import java.sql.*;

public class Solution {
    public static void main(String[] args) throws SQLException {
        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            try (PreparedStatement statement = connection.prepareStatement("select v.name, count(*) as minions_count from villains as v join minions_db.minions_villains mv on v.id = mv.villain_id group by v.id having minions_count > 15 order by minions_count desc")) {
                try (ResultSet result = statement.executeQuery()) {
                    while (result.next()) {
                        String name = result.getString("name");
                        int minionsCount = result.getInt("minions_count");

                        System.out.printf("%s %d%n", name, minionsCount);
                    }
                }
            }
        }
    }
}
