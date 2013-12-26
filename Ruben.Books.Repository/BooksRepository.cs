using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ruben.Books.DataLayer;
using Ruben.Books.Domain;

namespace Ruben.Books.Repository
{

    public interface IBooksRepository : IEntityRepository<Book>
    {
        //Book CreateBook(Book book);
        //ICollection<Book> GetBooks(BooksFilter filter);
        //Book Find(int id);
        void MarkAsRead(int bookId, DateTime when, int? pagesRead);

    }

    public class BooksRepository : BaseEntityRepository<Book>, IBooksRepository
    {
        private readonly BooksContext _context;
   
        public BooksRepository(IUnitOfWork<BooksContext> unitOfWork): base(unitOfWork.Context)
        {
            _context = unitOfWork.Context;
        }

        public void MarkAsRead(int bookId, DateTime when, int? pagesRead = null)
        {
            var book = Find(bookId);

            if (book != null)
            {
                book.State = State.Modified;
                book.Readings.Add(new Reading()
                {
                    BookId = bookId,
                    Date = when,
                    PagesRead =pagesRead.HasValue? pagesRead.Value : book.Pages,
                    State = State.Added
                });

                InsertOrUpdateGraph(book);
            }
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