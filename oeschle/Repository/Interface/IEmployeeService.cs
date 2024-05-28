namespace oeschle.Repository.Interface
{
    public interface IEmployeeService<T>
    {
        Task<List<T>> Get();
        Task<T> Get(long id);
        Task<T> Get(string document_number);
        Task<bool> Save(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(long id);
    }
}
