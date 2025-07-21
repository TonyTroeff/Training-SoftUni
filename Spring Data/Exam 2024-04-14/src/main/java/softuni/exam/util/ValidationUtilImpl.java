package softuni.exam.util;

import jakarta.validation.ValidatorFactory;
import org.springframework.stereotype.Component;

import jakarta.validation.ConstraintViolation;
import jakarta.validation.Validation;
import jakarta.validation.Validator;
import java.util.Set;

@Component
public class ValidationUtilImpl implements ValidationUtil {

    private final Validator validator;

    public ValidationUtilImpl() {
        try (ValidatorFactory factory = Validation.buildDefaultValidatorFactory()) {
            validator = factory.getValidator();
        }
    }

    @Override
    public <E> boolean isValid(E entity) {
        Set<ConstraintViolation<E>> validate = validator.validate(entity);
        return validate.isEmpty();
    }
}
