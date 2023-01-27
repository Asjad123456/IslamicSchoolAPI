using IslamicSchool.Data;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IslamicSchool.Repository
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : class
    {
        private readonly DataContext context;

        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            context.Set<T>().Where(expression).AsNoTracking();
    }
}
