package dtos;

import java.math.BigDecimal;

public record DepartmentSummaryDto(int id, String name, BigDecimal maxSalary) {
}
