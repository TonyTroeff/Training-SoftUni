package lab_1.dtos;

import java.util.List;

public class ManagerDto extends EmployeeDto {
    private List<EmployeeDto> subordinates;

    public List<EmployeeDto> getSubordinates() {
        return this.subordinates;
    }

    public void setSubordinates(List<EmployeeDto> subordinates) {
        this.subordinates = subordinates;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append(String.format("%s | Employees: %d", super.toString(), this.subordinates.size()));

        for (EmployeeDto employee : this.subordinates) {
            sb.append(System.lineSeparator());
            sb.append(String.format("- %s", employee.toString()));
        }

        return sb.toString();
    }
}
