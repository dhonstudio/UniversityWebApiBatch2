using Domain.Common;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        List<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        List<T> Find(Expression<Func<T, bool>> query);
    }
}
