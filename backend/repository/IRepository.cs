namespace repository;

public interface IRepository<T> where T : Task
{
  T GetById(int id);
  IEnumerable<T> List();
  IEnumerable<T> List(Expression<Func<T, bool>> predicate);
  void Add(T entity);
  void Delete(T entity);
  void Edit(T entity);
}
