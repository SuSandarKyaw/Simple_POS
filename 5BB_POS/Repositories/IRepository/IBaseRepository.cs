namespace _5BB_POS.Repositories.IRepository;

public interface IBaseRepository<T>
{
    Task<List<T>> Index();
    Task<T?> Show(int id);
    Task<T> Store(T data);
    Task<T> Update(T data);
    Task Delete(int id);
}
