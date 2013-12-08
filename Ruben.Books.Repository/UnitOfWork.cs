using Ruben.Books.DataLayer;

namespace Ruben.Books.Repository
{
    public class UnitOfWork : IUnitOfWork<BooksContext>
    {
        private readonly BooksContext _context;


        public UnitOfWork()
        {
            _context = new BooksContext();
        }

        public UnitOfWork(BooksContext context)
        {
            _context = context;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public BooksContext Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}