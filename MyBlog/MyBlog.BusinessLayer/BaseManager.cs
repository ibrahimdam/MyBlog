using MyBlog.DataAccessLayer.Abstract;
using MyBlog.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BusinessLayer
{
    public abstract class BaseManager<T> : IRepository<T> where T : class
    {
        private GenericRepository<T> _repository = new GenericRepository<T>();
        public virtual int Delete(T entity)
        {
            return _repository.Delete(entity);
        }

        public virtual T Find(Expression<Func<T, bool>> filter)
        {
            return _repository.Find(filter);
        }

        public virtual T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual int Insert(T entity)
        {
            return _repository.Insert(entity);
        }

        public virtual List<T> List()
        {
            return _repository.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> filter)
        {
            return _repository.List(filter);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return _repository.ListQueryable();
        }

        public virtual IQueryable<T> ListQueryable(Expression<Func<T, bool>> filter)
        {
            return _repository.ListQueryable(filter);
        }

        public virtual int Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
