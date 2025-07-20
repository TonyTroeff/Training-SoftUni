package softuni.exam.database;
//TestDbPrimaryKeys
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
public class TestDbPrimaryKeys {

   @Autowired
   private JdbcTemplate jdbcTemplate;

   public JdbcTemplate getJdbcTemplate() {
       return jdbcTemplate;
   }


   @Test
   void testPrimaryKeys() throws SQLException {
       DatabaseMetaData metaData = getDatabaseMetaData();

       ResultSet primaryKeyAttractions = metaData.getPrimaryKeys(null, null, "ATTRACTIONS");
       ResultSet primaryKeyCountries = metaData.getPrimaryKeys(null, null, "COUNTRIES");
       ResultSet primaryKeyPersonalData = metaData.getPrimaryKeys(null, null, "PERSONAL_DATAS");
       ResultSet primaryKeyVisitors = metaData.getPrimaryKeys(null, null, "VISITORS");

       List<String> actualResult = new ArrayList<>();

       primaryKeyAttractions.next();
       actualResult.add(primaryKeyAttractions.getString("TABLE_NAME"));
       actualResult.add(primaryKeyAttractions.getString("COLUMN_NAME"));

       primaryKeyCountries.next();
       actualResult.add(primaryKeyCountries.getString("TABLE_NAME"));
       actualResult.add(primaryKeyCountries.getString("COLUMN_NAME"));

       primaryKeyPersonalData.next();
       actualResult.add(primaryKeyPersonalData.getString("TABLE_NAME"));
       actualResult.add(primaryKeyPersonalData.getString("COLUMN_NAME"));

       primaryKeyVisitors.next();
       actualResult.add(primaryKeyVisitors.getString("TABLE_NAME"));
       actualResult.add(primaryKeyVisitors.getString("COLUMN_NAME"));

       List<String> expectedResult = new ArrayList<>();

       expectedResult.add("ATTRACTIONS");
       expectedResult.add("ID");
       expectedResult.add("COUNTRIES");
       expectedResult.add("ID");
       expectedResult.add("PERSONAL_DATAS");
       expectedResult.add("ID");
       expectedResult.add("VISITORS");
       expectedResult.add("ID");

       Assertions.assertArrayEquals(expectedResult.stream().sorted().toArray(), actualResult.stream().sorted().toArray());
   }

   private DatabaseMetaData getDatabaseMetaData() throws SQLException {
       DataSource dataSource = getJdbcTemplate().getDataSource();
       Connection connection = DataSourceUtils.getConnection(dataSource);
       return connection.getMetaData();
   }
}