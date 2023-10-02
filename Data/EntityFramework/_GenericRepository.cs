using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Data.Abstract;
using Data.Context;
using Data.Custom;

namespace Data.EntityFramework
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> dbset;
        protected Db db;
        protected Response _response;
        public GenericRepository()
        {
            db = new Db();
            dbset = db.Set<T>();
            _response = new Response();
        }

        public Response AddRange(List<T> items, int CurrentUserId)
        {
            try
            {
                dbset.AddRange(items);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıtlar Eklendi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response AddRange(List<T> items)
        {
            try
            {
                dbset.AddRange(items);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıtlar Eklendi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response Delete(T p, int CurrentUserId)
        {
            try
            {
                dbset.Remove(p);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıt Silindi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response Delete(T p)
        {
            try
            {
                dbset.Remove(p);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıt Silindi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public virtual T Get(int id)
        {
            return dbset.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            return dbset.FirstOrDefault(filter);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbset.AsQueryable();
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return dbset.Where(filter).AsQueryable();
        }

        public Response Insert(T p, int CurrentUserId)
        {
            try
            {
                dbset.Add(p);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıt Eklendi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response Insert(T p)
        {
            try
            {
                dbset.Add(p);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıt Eklendi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response RemoveRange(List<T> items, int CurrentUserId)
        {
            try
            {
                dbset.RemoveRange(items);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıtlar Silindi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response RemoveRange(List<T> items)
        {
            try
            {
                dbset.RemoveRange(items);
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıtlar Silindi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response Update(T p, int CurrentUserId)
        {
            try
            {
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıt Kaydedildi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }

        public Response Update(T p)
        {
            try
            {
                db.SaveChanges();
                _response.Success = true;
                _response.Description = "Kayıt Kaydedildi";
                return _response;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Description = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return _response;
            }
        }
    }
}
