package softuni.exam.service;

import java.io.IOException;
import jakarta.xml.bind.JAXBException;

public interface VisitorService {

    boolean areImported();

    String readVisitorsFileContent() throws IOException, JAXBException;

    String importVisitors() throws IOException, JAXBException;

}
