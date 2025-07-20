package softuni.exam.models.dto;

public class AttractionExportDto {
    private final Long id;
    private final String name;
    private final String description;
    private final Integer elevation;
    private final String countryName;

    public AttractionExportDto(Long id, String name, String description, Integer elevation, String countryName) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.elevation = elevation;
        this.countryName = countryName;
    }

    public Long getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public String getDescription() {
        return description;
    }

    public Integer getElevation() {
        return elevation;
    }

    public String getCountryName() {
        return countryName;
    }
}
