package exercise_1.dtos;

import jakarta.validation.constraints.*;

public class UserRegisterDto {
    @NotBlank(message = "The email is required.")
    @Email(message = "The email is not valid.")
    private final String email;

    @NotBlank(message = "The password is required.")
    @Size(min = 6, message = "The password must be at least 6 characters long.")
    @Pattern(regexp = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).*$")
    private final String password;

    @NotBlank(message = "The full name is required.")
    private final String fullName;

    public UserRegisterDto(String email, String password, String fullName) {
        this.email = email;
        this.password = password;
        this.fullName = fullName;
    }

    public String getEmail() {
        return this.email;
    }

    public String getPassword() {
        return this.password;
    }

    public String getFullName() {
        return this.fullName;
    }
}
