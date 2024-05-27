using Kampus.Data.Abstract;
using Kampus.Model.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Kampus.Data.Repositories
{
    public class EntityBaseRepository<T, K> : IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
        where K : DbContext
    {
        private readonly K _context;

        public EntityBaseRepository(K context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public T GetSingle(int id)
        {
            return _context.Set<T>().Where(v => v.Id == id).FirstOrDefault();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void DeleteRange(List<T> entity)
        {
            _context.RemoveRange(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
