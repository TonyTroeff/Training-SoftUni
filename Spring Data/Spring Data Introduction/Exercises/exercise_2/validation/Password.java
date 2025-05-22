package exercise_2.validation;

import jakarta.validation.Constraint;

import java.lang.annotation.*;

@Documented
@Target({ElementType.FIELD, ElementType.METHOD})
@Retention(RetentionPolicy.RUNTIME)
@Constraint(validatedBy = PasswordValidator.class)
public @interface Password {
    int minLength();

    boolean requireDigits();

    String message() default "Invalid password!";

    Class<?>[] groups() default {};

    Class<? extends Annotation>[] payload() default {};
}
