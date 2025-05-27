package lab_1.dtos;

import java.util.List;
import java.util.stream.Collectors;

public class SubordinateDto extends EmployeeDto {
    private List<EmployeeDto> managers;

    public List<EmployeeDto> getManagers() {
        return this.managers;
    }

    public void setManagers(List<EmployeeDto> managers) {
        this.managers = managers;
    }

    @Override
    public String toString() {
        String managersInfo = this.managers.stream()
                .map(x -> String.format("%s %s", x.getFirstName(), x.getLastName()))
                .collect(Collectors.joining(", "));
        return String.format("%s | Managers: %s", super.toString(), managersInfo);
    }
}
