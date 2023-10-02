using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Data.Entities.ExFit;

namespace Data.EntityFramework.ExFit
{
    public class DietRepository : GenericRepository<Diet>
    {
        private IQueryable<Diet> Includes()
        {
            return dbset
                .Include(x => x.Food)
                .AsQueryable();
        }

        public override Diet Get(int id)
        {
            return Includes().FirstOrDefault(x => x.DietId == id);
        }

        public override Diet Get(Expression<Func<Diet, bool>> filter)
        {
            return Includes().FirstOrDefault(filter);
        }

        public override IQueryable<Diet> GetAll()
        {
            return Includes();
        }

        public override IQueryable<Diet> GetAll(Expression<Func<Diet, bool>> filter)
        {
            return Includes().Where(filter);
        }
    }
}
