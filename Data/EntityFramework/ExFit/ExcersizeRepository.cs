using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Data.Entities.ExFit;

namespace Data.EntityFramework.ExFit
{
    public class ExcersizeRepository : GenericRepository<Excersize>
    {
        private IQueryable<Excersize> Includes()
        {
            return dbset
                .Include(x => x.Practice)
                .AsQueryable();
        }

        public override Excersize Get(int id)
        {
            return Includes().FirstOrDefault(x => x.ExcersizeId == id);
        }

        public override Excersize Get(Expression<Func<Excersize, bool>> filter)
        {
            return Includes().FirstOrDefault(filter);
        }

        public override IQueryable<Excersize> GetAll()
        {
            return Includes();
        }

        public override IQueryable<Excersize> GetAll(Expression<Func<Excersize, bool>> filter)
        {
            return Includes().Where(filter);
        }
    }
}
