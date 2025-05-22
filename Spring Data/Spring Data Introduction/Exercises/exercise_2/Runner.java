package exercise_2;

import exercise_2.models.User;
import exercise_2.services.UserService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
public class Runner implements CommandLineRunner {
    private final UserService userService;

    public Runner(UserService userService) {
        this.userService = userService;
    }

    @Override
    public void run(String... args) {
        User me = this.userService.registerUser("Tony", "Troeff", "troeff", "123456-Aa", "tonyslav_troeff@outlook.com", 23);

        // NOTE: These users are AI-generated :D
        User u1 = this.userService.registerUser("Anna", "Stark", "a.stark22", "passWord123!", "anna.stark22@gmail.com", 28);
        User u2 = this.userService.registerUser("Brian", "Shepard", "bri_shep", "SuperBri90#", "bri.shepard_90@gmail.com", 33);
        User u3 = this.userService.registerUser("Clara", "Weston", "clara-west", "Pass!2023", "clara-weston@softuni.bg", 26);
        User u4 = this.userService.registerUser("David", "Nguyen", "dave.n", "DAVid.321$", "dave.n_01@protonmail.com", 21);
        User u5 = this.userService.registerUser("Elena", "Boyd", "el_boyd", "boydRulez#55", "el.boyd88@softuni.bg", 35);
        User u6 = this.userService.registerUser("Frank", "Maier", "frank-maier", "Frankie!41", "frank.maier@softuni.bg", 41);
        User u7 = this.userService.registerUser("Gina", "Rosetti", "ros.gina", "GRosetti77!", "g.rossetti77@intoprogramming.info", 29);
        User u8 = this.userService.registerUser("Hector", "Fischer", "hfischer", "h3cTor_F!", "hfischer@softuni.bg", 24);
        User u9 = this.userService.registerUser("Ivy", "Chambers", "ivy.chambers", "IvyCh@mbers1", "ivy.chambers@github.com", 37);
        User u10 = this.userService.registerUser("Jan", "Peterson", "j.peterson", "JanPet12@", "j.peterson@mail.uu.net", 23);
        User u11 = this.userService.registerUser("Katerina", "Ivanova", "k.ivanova", "K8rina!987", "k.ivanova@softuni.bg", 31);
        User u12 = this.userService.registerUser("Lukas", "Bauer", "lukas-b", "BaUer45!*", "lukas-bauer@softuni.bg", 25);
        User u13 = this.userService.registerUser("Maria", "Todorova", "maria_555", "MariaT!555", "maria_555@coding.bg", 27);
        User u14 = this.userService.registerUser("Nina", "Keller", "nina-keller", "Keller123$", "nina.keller@gmail.com", 22);
        User u15 = this.userService.registerUser("Oscar", "Silva", "oscars", "Oscar!321", "oscar.silva@hotmail.co.uk", 30);

        // We should not be able to create a user with invalid email.
        // this.userService.registerUser("Lucas", "Moore", "lucasmoore", "ValidPass1!", "lucasmoore.mail.com", 27);

        // We should not be able to create a user with invalid email.
        // this.userService.registerUser("Emma", "Brooks", "emmabrooks", "123", "emma.brooks@example.com", 29);
        // this.userService.registerUser("Emma", "Brooks", "emmabrooks", "thisisemmanodigits", "emma.brooks@example.com", 29);

        this.showUsers("gmail.com");
        this.showUsers("softuni.bg");
    }

    private void showUsers(String domain) {
        List<User> users = this.userService.findByDomain(domain);

        System.out.printf("There are %d users with emails from \"%s\".%n", users.size(), domain);
        for (User user : users)
            System.out.printf("%d. %s%n", user.getId(), user.getEmail());
    }
}
