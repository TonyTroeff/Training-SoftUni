package softuni.exam.database;

//TestDbPersonalDataTable
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.datasource.DataSourceUtils;

import javax.sql.DataSource;
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;


@DataJpaTest
public class TestDbPersonalDataTable {

   @Autowired
   private JdbcTemplate jdbcTemplate;

   public JdbcTemplate getJdbcTemplate() {
       return jdbcTemplate;
   }

   @Test
   void testDevicesTable() throws SQLException {
       DatabaseMetaData metaData = getDatabaseMetaData();
       List<String> expectedResult = new ArrayList<>();
       expectedResult.add("ID,BIGINT,0,null,64");
       expectedResult.add("BIRTH_DATE,DATE,1,null,10");
       expectedResult.add("CARD_NUMBER,CHARACTER VARYING,0,null,255");
       expectedResult.add("GENDER,CHARACTER,1,null,1");
       expectedResult.add("AGE,INTEGER,1,null,32");


       ResultSet columns = metaData.getColumns(null, "PUBLIC", "PERSONAL_DATAS", null);

       final List<String> actualResult = new ArrayList<>();

       while (columns.next()) {
           actualResult.add(String.format("%s,%s,%s,%s,%s",
                   columns.getString("COLUMN_NAME"),
                   columns.getString("TYPE_NAME"),
                   columns.getString("NULLABLE"),
                   columns.getString("BUFFER_LENGTH"),
                   columns.getString("COLUMN_SIZE")
           ));
       }

       Assertions.assertArrayEquals(expectedResult.stream().sorted().toArray(), actualResult.stream().sorted().toArray());
   }

   private DatabaseMetaData getDatabaseMetaData() throws SQLException {
       DataSource dataSource = getJdbcTemplate().getDataSource();
       Connection connection = DataSourceUtils.getConnection(dataSource);
       return connection.getMetaData();
   }
}