package orm;

import java.util.List;

public interface DbContext<E> {
    boolean persist(E entity);

    List<E> find(Class<E> entityClass);
    List<E> find(Class<E> entityClass, String where);

    E findFirst(Class<E> entityClass);
    E findFirst(Class<E> entityClass, String where);
}
