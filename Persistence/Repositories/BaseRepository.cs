using Application.Interfaces.Repositories;
using Domain.Common;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly SchoolContext SchoolContext;

        public BaseRepository(SchoolContext schoolContext)
        {
            SchoolContext = schoolContext;
        }

        public T Add(T entity)
        {
            SchoolContext.Set<T>().Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            SchoolContext.Set<T>().Remove(entity);
            return entity;
        }

        public List<T> Find(Expression<Func<T, bool>> query)
        {
            return SchoolContext.Set<T>().Where(query).ToList();
        }

        public List<T> GetAll()
        {
            return SchoolContext.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return SchoolContext.Set<T>().Find(id);
        }

        public T Update(T entity)
        {
            SchoolContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
