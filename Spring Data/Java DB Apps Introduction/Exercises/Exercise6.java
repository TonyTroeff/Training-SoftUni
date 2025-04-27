import java.sql.*;
import java.util.ArrayList;

public class Exercise6 {
    public static void main(String[] args) throws SQLException {
        try (Connection connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/minions_db", "root", "root")) {
            // Choose one of the following options.
            solve1(connection);
            solve2(connection);
        }
    }

    private static void solve1(Connection connection) throws SQLException {
        try (PreparedStatement statement = connection.prepareStatement("select m.name from minions as m", ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY)) {
            try (ResultSet result = statement.executeQuery()) {
                int count = 0;
                while (result.next()) count++;

                for (int i = 0; i < count; i++) {
                    if (i % 2 == 0) result.absolute(i / 2 + 1);
                    else result.absolute(count - (i - 1) / 2);

                    String name = result.getString("name");
                    System.out.printf("%s%n", name);
                }
            }
        }
    }

    private static void solve2(Connection connection) throws SQLException {
        ArrayList<String> minionsNames = new ArrayList<>();

        try (PreparedStatement statement = connection.prepareStatement("select m.name from minions as m")) {
            try (ResultSet result = statement.executeQuery()) {
                while (result.next()) minionsNames.add(result.getString("name"));
            }
        }

        for (int i = 0; i < minionsNames.size(); i++) {
            String currentName;
            if (i % 2 == 0) currentName = minionsNames.get(i / 2);
            else currentName = minionsNames.get(minionsNames.size() - (i + 1) / 2);

            System.out.printf("%s%n", currentName);
        }
    }
}
