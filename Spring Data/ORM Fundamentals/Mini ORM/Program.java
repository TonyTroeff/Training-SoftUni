import entities.User;
import orm.EntityManager;
import orm.MyConnector;

import java.sql.SQLException;
import java.time.LocalDate;

public class Program {
    public static void main(String[] args) throws SQLException {
        MyConnector connector = new MyConnector("mysql://localhost:3306", "root", "root", "miniorm");
        EntityManager<User> manager = new EntityManager<>(connector);

        User user = new User("Tony Troeff", 23, LocalDate.now());

        boolean createdSuccessfully = manager.persist(user);
        System.out.printf("A new user was created successfully: %s%n", createdSuccessfully);
        System.out.printf("User id: %d%n", user.getId());

        user.setAge(33);

        boolean updatedSuccessfully = manager.persist(user);
        System.out.printf("User age was updated successfully: %s%n", updatedSuccessfully);

        System.out.println();
        System.out.println("All users:");
        Iterable<User> users = manager.find(User.class);
        for (User u : users) {
            System.out.printf("User id: %d%n", u.getId());
            System.out.printf("User username: %s%n", u.getUsername());
            System.out.printf("User age: %d%n", u.getAge());
            System.out.printf("User registration date: %s%n", u.getRegistration());
            System.out.println();
        }
    }
}
