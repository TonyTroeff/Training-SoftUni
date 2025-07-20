package softuni.exam.database;
//TestDbForeignKeysDevices
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
public class TestDbForeignKeysVisitors {

   @Autowired
   private JdbcTemplate jdbcTemplate;

   public JdbcTemplate getJdbcTemplate() {
       return jdbcTemplate;
   }

   @Test
   void testForeignKeysVisitors() throws SQLException {
       DatabaseMetaData metaData = getDatabaseMetaData();

       ResultSet attractionsKeys = metaData.getImportedKeys(null, null, "ATTRACTIONS");
       ResultSet visitorsKeys = metaData.getImportedKeys(null, null, "VISITORS");

       List<String> actualResult = new ArrayList<>();
       attractionsKeys.next();
       actualResult.add(attractionsKeys.getString("PKTABLE_NAME"));
       actualResult.add(attractionsKeys.getString("PKCOLUMN_NAME"));
       actualResult.add(attractionsKeys.getString("PKCOLUMN_NAME"));
       actualResult.add(attractionsKeys.getString("FKCOLUMN_NAME"));


       visitorsKeys.next();
       actualResult.add(visitorsKeys.getString("PKTABLE_NAME"));
       actualResult.add(visitorsKeys.getString("PKCOLUMN_NAME"));
       actualResult.add(visitorsKeys.getString("PKCOLUMN_NAME"));
       actualResult.add(visitorsKeys.getString("FKCOLUMN_NAME"));



       List<String> expectedResult = new ArrayList<>();
       expectedResult.add("ATTRACTION_ID");
       expectedResult.add("ATTRACTIONS");
       expectedResult.add("COUNTRY_ID");
       expectedResult.add("COUNTRIES");
       expectedResult.add("ID");
       expectedResult.add("ID");
       expectedResult.add("ID");
       expectedResult.add("ID");

       Assertions.assertArrayEquals(expectedResult.stream().sorted().toArray(), actualResult.stream().sorted().toArray());
   }

   private DatabaseMetaData getDatabaseMetaData() throws SQLException {
       DataSource dataSource = getJdbcTemplate().getDataSource();
       Connection connection = DataSourceUtils.getConnection(dataSource);
       return connection.getMetaData();
   }
}