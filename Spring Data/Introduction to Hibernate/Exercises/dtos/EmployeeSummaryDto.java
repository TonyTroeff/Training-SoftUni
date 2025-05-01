package dtos;

import java.math.BigDecimal;

public record EmployeeSummaryDto(int id, String firstName, String lastName, String jobTitle, BigDecimal salary) {
}
