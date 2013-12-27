using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Ruben.Books.DataLayer;

namespace Ruben.Books.Repository
{
    public interface IEntityRepository<T> : IDisposable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(int id);       
        void InsertOrUpdate(T entity);
        void InsertOrUpdateGraph(T entity);
        void Delete(int id);
    }

    public abstract class BaseEntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly DbContext _context;
        public BaseEntityRepository(DbContext context)
        {
            _context = context;
        }
        protected abstract DbSet<T> GetEntities();
        protected abstract int GetId(T entity);

        public IQueryable<T> All
        {
            get { return GetEntities(); }
        }

       
        //public List<Customer> AllCustomersWhoHaveOrdered
        //{
        //    get { return _context.Customers.Where(c => c.Orders.Any()).ToList(); }
        //}

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetEntities();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T Find(int id)
        {
            return GetEntities().Find(id);
        }       

        public void InsertOrUpdateGraph(T graph)
        {
            GetEntities().Add(graph);
            _context.ApplyStateChanges();
        }

        public void InsertOrUpdate(T book)
        {
            if (GetId(book) == default(int)) // New entity
            {
                _context.Entry(book).State = EntityState.Added;
            }
            else        // Existing entity
            {
                _context.Entry(book).State = EntityState.Modified;

            }
        }

        public void Delete(int id)
        {
            var book = GetEntities().Find(id);
            GetEntities().Remove(book);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}