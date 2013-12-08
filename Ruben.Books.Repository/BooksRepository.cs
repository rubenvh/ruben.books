using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ruben.Books.DataLayer;
using Ruben.Books.Domain;

namespace Ruben.Books.Repository
{

    interface IBooksRepository : IEntityRepository<Book>
    {
        //Book CreateBook(Book book);
        //ICollection<Book> GetBooks(BooksFilter filter);
        //Book Find(int id);
        //void MarkRead(int bookId, DateTime when);

    }

    public class BooksRepository : BaseEntityRepository<Book>, IBooksRepository
    {
        private readonly BooksContext _context;
   
        public BooksRepository(UnitOfWork uow): base(uow.Context)
        {
            _context = uow.Context;
        }
              

        public List<Book> AllBooks
        {
            get { return _context.Books.ToList(); }
        }
        //public List<Customer> AllCustomersWhoHaveOrdered
        //{
        //    get { return _context.Customers.Where(c => c.Orders.Any()).ToList(); }
        //}
        

        protected override DbSet<Book> GetEntities()
        {
            return _context.Books;
        }

        protected override int GetId(Book entity)
        {
            return entity.Id;
        }
    }
}