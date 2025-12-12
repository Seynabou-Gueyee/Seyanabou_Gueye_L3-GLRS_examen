package repository;
import java.util.List;
public interface IGenericRepository<T> {
    T save(T entity);
    List<T> findAll();
}
