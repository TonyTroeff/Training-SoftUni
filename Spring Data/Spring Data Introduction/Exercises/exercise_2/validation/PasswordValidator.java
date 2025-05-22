package exercise_2.validation;

import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;

public class PasswordValidator implements ConstraintValidator<Password, String> {
    private Password annotation;

    @Override
    public void initialize(Password annotation) {
        this.annotation = annotation;
    }

    @Override
    public boolean isValid(String value, ConstraintValidatorContext context) {
        if (value == null) return false;

        if (value.length() < this.annotation.minLength()) return false;
        if (this.annotation.requireDigits() && value.chars().noneMatch(Character::isDigit)) return false;

        return true;
    }
}
