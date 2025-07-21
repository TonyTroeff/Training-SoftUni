package softuni.exam.util;

import jakarta.xml.bind.JAXBException;

public interface XmlParser {
    <T> T parse(String text, Class<T> tClass) throws JAXBException;
}
