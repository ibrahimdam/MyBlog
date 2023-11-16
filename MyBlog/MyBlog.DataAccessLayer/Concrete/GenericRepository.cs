using Microsoft.EntityFrameworkCore;
using MyBlog.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccessLayer.Concrete
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        MyBlogContext _context = new MyBlogContext();
        private DbSet<TEntity> _object;

        public GenericRepository()
        {
            _object= _context.Set<TEntity>();
        }

        public int Delete(TEntity entity)
        {
            _object.Remove(entity);
            return _context.SaveChanges();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return _object.FirstOrDefault(filter);
        }

        public TEntity GetById(int id)
        {
            return _object.Find(id);
        }

        public int Insert(TEntity entity)
        {
            _object.Add(entity);
            return _context.SaveChanges();
        }

        public List<TEntity> List()
        {
            return _object.ToList();
        }

        public List<TEntity> List(Expression<Func<TEntity, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public IQueryable<TEntity> ListQueryable()
        {
            return _object.AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> ListQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return _object.Where(filter);
        }

        public int Update(TEntity entity)
        {
            _object.Update(entity);
            return _context.SaveChanges();
        }
    }
}
