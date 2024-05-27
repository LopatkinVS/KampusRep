using Kampus.Model.Abstract;

namespace Kampus.Data.Abstract
{
    public interface IEntityBaseRepository<T> 
        where T : class, IEntityBase, new()
    {
        IEnumerable<T> GetAll();
        int Count();
        T GetSingle(int id);
        T GetSingle(string name);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entity);
        void Save();
    }
}
