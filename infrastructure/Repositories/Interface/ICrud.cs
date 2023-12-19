namespace infrastructure.Repositories.Interface
{
    public interface ICrud<T>
    {
        T Create(T item);
        
        T Read(int id);
        
        void Update(T item);
        
        void Delete(int id);
    }
}