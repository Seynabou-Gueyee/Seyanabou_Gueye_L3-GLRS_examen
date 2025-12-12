package repository;
import java.util.List;
public class IGenericRepository {
    T save(T entity);
    List<T> findAll();
}
