package softuni.exam.service;

import softuni.exam.models.entity.Sale;

import java.io.IOException;

public interface SaleService {
    boolean areImported();

    String readSalesFileContent() throws IOException;

    String importSales() throws IOException;

    Sale getReferenceById(Long id);
}
