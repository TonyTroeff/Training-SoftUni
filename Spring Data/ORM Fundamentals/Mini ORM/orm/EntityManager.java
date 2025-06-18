package orm;

import orm.annotations.Column;
import orm.annotations.Entity;
import orm.annotations.Id;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.sql.*;
import java.time.LocalDate;
import java.util.*;
import java.util.function.Function;
import java.util.stream.Collectors;

public class EntityManager<E> implements DbContext<E> {
    private static final String DEFAULT_ID = "id";
    private final MyConnector connector;

    public EntityManager(MyConnector connector) {
        this.connector = connector;
    }

    @Override
    public boolean createTable(Class<E> entityClass, boolean ifNotExists) {
        String tableName = this.getTableName(entityClass);

        Field idField = this.getIdField(entityClass);
        String idColumnName = this.getIdColumnName(idField);

        Map<String, Field> columns = this.getColumns(entityClass, f -> !f.equals(idField));

        try {
            StringBuilder query = new StringBuilder();

            query.append("create table ");
            if (ifNotExists) query.append("if not exists ");
            query.append(tableName).append(" ");

            query.append("(").append(idColumnName).append(" bigint not null auto_increment primary key");

            for (Map.Entry<String, Field> entry : columns.entrySet()) {
                String columnName = entry.getKey();
                Field field = entry.getValue();

                query.append(", ").append(columnName).append(" ").append(this.prepareType(field.getType()));
            }

            query.append(")");

            executeQuery(query.toString());

            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean addMissingColumns(Class<E> entityClass) {
        String tableName = this.getTableName(entityClass);
        Field idField = this.getIdField(entityClass);
        Map<String, Field> columns = this.getColumns(entityClass, f -> !f.equals(idField));

        try {
            Set<String> columnsInDatabase = this.getColumnsInDatabase(tableName);
            List<String> addColumnDefinitions = new ArrayList<>();

            for (Map.Entry<String, Field> entry : columns.entrySet()) {
                String columnName = entry.getKey();
                if (columnsInDatabase.contains(columnName)) continue;

                Field field = entry.getValue();
                addColumnDefinitions.add(String.format("%s %s", columnName, this.prepareType(field.getType())));
            }

            if (addColumnDefinitions.isEmpty()) return true;

            String query = String.format("alter table %s add %s", tableName, String.join(", ", addColumnDefinitions));
            executeQuery(query);

            return true;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean persist(E entity) {
        try {
            Field idField = this.getIdField(entity.getClass());
            Object idValue = this.getValue(idField, entity);
            return (idValue == null) ? this.insert(idField, entity) : this.update(idField, idValue, entity);
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public boolean delete(E entity) {
        Class<?> entityClass = entity.getClass();

        String tableName = this.getTableName(entityClass);
        Field idField = this.getIdField(entityClass);
        String idColumnName = this.getIdColumnName(idField);

        try {
            Object idValue = this.getValue(idField, entity);
            String query = String.format("delete from %s where %s = %s", tableName, idColumnName, this.prepareValue(idValue));

            Connection connection = this.connector.getConnection();
            PreparedStatement statement = connection.prepareStatement(query);
            return statement.executeUpdate() == 1;
        } catch (Exception e) {
            return false;
        }
    }

    @Override
    public List<E> find(Class<E> entityClass) {
        String tableName = this.getTableName(entityClass);
        String query = String.format("select * from %s", tableName);

        return this.findInternally(entityClass, query);
    }

    @Override
    public List<E> find(Class<E> entityClass, String where) {
        String tableName = this.getTableName(entityClass);
        String query = String.format("select * from %s where %s", tableName, where);

        return this.findInternally(entityClass, query);
    }

    @Override
    public E findFirst(Class<E> entityClass) {
        String tableName = this.getTableName(entityClass);
        String query = String.format("select * from %s limit 1", tableName);

        List<E> result = this.findInternally(entityClass, query);
        if (result.isEmpty()) return null;
        return result.getFirst();
    }

    @Override
    public E findFirst(Class<E> entityClass, String where) {
        String tableName = this.getTableName(entityClass);
        String query = String.format("select * from %s where %s limit 1", tableName, where);

        List<E> result = this.findInternally(entityClass, query);
        if (result.isEmpty()) return null;
        return result.getFirst();
    }

    private List<E> findInternally(Class<E> entityClass, String query) {
        Map<String, Field> columns = this.getColumns(entityClass);

        try {
            Connection connection = this.connector.getConnection();
            PreparedStatement statement = connection.prepareStatement(query);

            ResultSet resultSet = statement.executeQuery();
            List<E> entities = new ArrayList<>();

            while (resultSet.next()) {
                E currentEntity = this.createEntity(entityClass, resultSet, columns);
                entities.add(currentEntity);
            }

            return entities;
        } catch (Exception e) {
            return Collections.emptyList();
        }
    }

    private boolean insert(Field idField, E entity) throws IllegalAccessException, SQLException {
        String tableName = this.getTableName(entity.getClass());
        Map<String, Field> columns = this.getColumns(entity.getClass(), f -> !f.equals(idField));

        StringBuilder query = new StringBuilder();
        query.append("insert into ").append(tableName).append(" ");
        query.append("(").append(String.join(", ", columns.keySet())).append(")");

        List<Object> values = new ArrayList<>();
        for (Field field : columns.values()) values.add(this.getValue(field, entity));

        query.append(" values ").append("(").append(values.stream().map(this::prepareValue).collect(Collectors.joining(", "))).append(")");

        Connection connection = this.connector.getConnection();
        PreparedStatement statement = connection.prepareStatement(query.toString(), Statement.RETURN_GENERATED_KEYS);
        if (statement.executeUpdate() == 0) return false;

        ResultSet generatedKeys = statement.getGeneratedKeys();
        if (!generatedKeys.next())
            throw new IllegalStateException("Could not obtain the auto-generated identifier of the inserted entity.");

        Object idValue = generatedKeys.getLong(1);
        this.setValue(idField, entity, idValue);
        return true;
    }

    private boolean update(Field idField, Object idValue, E entity) throws IllegalAccessException, SQLException {
        String tableName = this.getTableName(entity.getClass());
        Map<String, Field> columns = this.getColumns(entity.getClass(), f -> !f.equals(idField));

        StringBuilder query = new StringBuilder();
        query.append("update ").append(tableName).append(" set ");

        Map<String, Object> values = new HashMap<>();

        for (Map.Entry<String, Field> entry : columns.entrySet()) {
            Field field = entry.getValue();
            if (field.equals(idField)) continue;

            String columnName = entry.getKey();
            Object value = this.getValue(field, entity);
            values.put(columnName, value);
        }

        String changes = values.entrySet().stream()
                .map(x -> String.format("%s = %s", x.getKey(), this.prepareValue(x.getValue())))
                .collect(Collectors.joining(", "));
        query.append(changes);

        String idColumnName = this.getIdColumnName(idField);
        query.append(" where ").append(idColumnName).append(" = ").append(idValue);

        Connection connection = this.connector.getConnection();
        PreparedStatement statement = connection.prepareStatement(query.toString());
        return statement.executeUpdate() == 1;
    }

    private Set<String> getColumnsInDatabase(String tableName) throws SQLException {
        Connection connection = this.connector.getConnection();
        PreparedStatement statement = connection.prepareStatement("select column_name from information_schema.columns where table_schema = ? and table_name = ?");

        statement.setString(1, this.connector.getDatabaseName());
        statement.setString(2, tableName);

        ResultSet resultSet = statement.executeQuery();
        Set<String> columns = new HashSet<>();
        while (resultSet.next()) columns.add(resultSet.getString("column_name"));

        return columns;
    }

    private void executeQuery(String query) throws SQLException {
        Connection connection = this.connector.getConnection();
        PreparedStatement statement = connection.prepareStatement(query);
        statement.execute();
    }

    private Field getIdField(Class<?> entityClass) {
        List<Field> idFields = Arrays.stream(entityClass.getDeclaredFields()).filter(x -> x.isAnnotationPresent(Id.class)).toList();
        if (idFields.size() > 1) throw new IllegalStateException("Entity has more than one primary key.");
        if (idFields.isEmpty()) throw new IllegalStateException("Entity does not have primary key.");

        Field idField = idFields.getFirst();
        if (idField.getType() != Long.class) throw new IllegalStateException("Primary key must be long.");

        return idField;
    }

    private Object getValue(Field field, E entity) throws IllegalAccessException {
        field.setAccessible(true);
        return field.get(entity);
    }

    private void setValue(Field field, E entity, Object value) throws IllegalAccessException {
        field.setAccessible(true);
        field.set(entity, value);
    }

    private String getTableName(Class<?> entityClass) {
        Entity annotation = entityClass.getAnnotation(Entity.class);
        if (annotation == null) throw new IllegalStateException("Entity type is not annotated.");

        return annotation.name();
    }

    private Map<String, Field> getColumns(Class<?> entityClass) {
        return this.getColumns(entityClass, f -> true);
    }

    private Map<String, Field> getColumns(Class<?> entityClass, Function<Field, Boolean> condition) {
        Map<String, Field> result = new HashMap<>();

        for (Field field : entityClass.getDeclaredFields()) {
            if (!condition.apply(field)) continue;

            Optional<String> columnName = this.getColumnName(field);
            columnName.ifPresent(s -> result.put(s, field));
        }

        return result;
    }

    private String getIdColumnName(Field idField) {
        return this.getColumnName(idField).orElse(DEFAULT_ID);
    }

    private Optional<String> getColumnName(Field field) {
        Column annotation = field.getAnnotation(Column.class);
        if (annotation == null) return Optional.empty();

        String name = annotation.name();
        if (name == null || name.isEmpty()) throw new IllegalStateException("Column name cannot be empty.");

        return Optional.of(name);
    }

    private E createEntity(Class<E> entityClass, ResultSet resultSet, Map<String, Field> columns) throws NoSuchMethodException, InvocationTargetException, InstantiationException, IllegalAccessException, SQLException {
        E entity = entityClass.getDeclaredConstructor().newInstance();
        for (Map.Entry<String, Field> entry : columns.entrySet()) {
            String columnName = entry.getKey();
            Field field = entry.getValue();

            Object value = null;
            if (field.getType() == Integer.class)
                value = resultSet.getInt(columnName);
            else if (field.getType() == Long.class)
                value = resultSet.getLong(columnName);
            else if (field.getType() == String.class)
                value = resultSet.getString(columnName);
            else if (field.getType() == LocalDate.class)
                value = resultSet.getDate(columnName).toLocalDate();

            this.setValue(field, entity, value);
        }

        return entity;
    }

    private String prepareValue(Object value) {
        if (value == null) return "null";
        if (value instanceof String || value instanceof LocalDate) return String.format("'%s'", value);
        return value.toString();
    }

    private String prepareType(Class<?> type) {
        if (type == Integer.class) return "int";
        if (type == Long.class) return "bigint";
        if (type == String.class) return "varchar(255)";
        if (type == LocalDate.class) return "date";

        throw new IllegalStateException("Unsupported type.");
    }
}
