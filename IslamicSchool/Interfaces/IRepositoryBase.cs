using System.Linq.Expressions;

namespace IslamicSchool.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
