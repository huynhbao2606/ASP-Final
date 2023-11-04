using ASP_Final.Models;
using System.Linq.Expressions;

namespace ASP_Final.Dao.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetEntityById(int? Id);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Save();

    }
}
