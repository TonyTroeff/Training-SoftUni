package exercise_2.utils;

import jakarta.xml.bind.annotation.adapters.XmlAdapter;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class LocalDateXmlAdapter extends XmlAdapter<String, LocalDate> {
    private static final DateTimeFormatter FORMATTER = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");

    @Override
    public LocalDate unmarshal(String v) {
        if (v == null || v.isEmpty()) return null;
        return LocalDate.parse(v, FORMATTER);
    }

    @Override
    public String marshal(LocalDate v) {
        if (v == null) return null;
        return v.atStartOfDay().format(FORMATTER);
    }
}
