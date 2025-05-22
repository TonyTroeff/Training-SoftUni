package exercise_2.validation;

import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;

public class EmailValidator implements ConstraintValidator<Email, String> {
    private static final String USER_REGEX = "[A-Za-z0-9]+([._-][A-Za-z0-9]+)*";
    private static final String HOST_WORD_REGEX = "[A-Za-z]+(-[A-Za-z]+)*";
    private static final String HOST_REGEX = "(" + HOST_WORD_REGEX + ")(\\." + HOST_WORD_REGEX + ")+";
    private static final String EMAIL_REGEX = "^" + USER_REGEX + "@" + HOST_REGEX + "$";

    @Override
    public boolean isValid(String s, ConstraintValidatorContext constraintValidatorContext) {
        if (s == null) return false;
        return s.matches(EMAIL_REGEX);

    }
}
