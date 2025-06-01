package exercise_1;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.PropertySource;

@SpringBootApplication
@PropertySource(value = "classpath:exercise_1.properties")
public class Solution {
    public static void main(String[] args) {
        SpringApplication.run(Solution.class, args);
    }
}
