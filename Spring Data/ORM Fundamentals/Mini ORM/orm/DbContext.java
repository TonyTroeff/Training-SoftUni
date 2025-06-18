package orm;

import java.util.List;

public interface DbContext<E> {
    boolean createTable(Class<E> entityClass, boolean ifNotExists);
    boolean addMissingColumns(Class<E> entityClass);

    boolean persist(E entity);

    List<E> find(Class<E> entityClass);
    List<E> find(Class<E> entityClass, String where);

    E findFirst(Class<E> entityClass);
    E findFirst(Class<E> entityClass, String where);
}
