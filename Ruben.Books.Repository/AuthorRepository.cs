using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ruben.Books.DataLayer;
using Ruben.Books.Domain;

namespace Ruben.Books.Repository
{

    public interface IAuthorRepository : IEntityRepository<Author> 
    {
        ICollection<Author> FindAuthorByName(string term);
    }

    public class AuthorRepository : BaseEntityRepository<Author>, IAuthorRepository
    {
        private readonly BooksContext _context;

        public AuthorRepository(IUnitOfWork<BooksContext> uow)
            : base(uow.Context)
        {
            _context = uow.Context;
        }

        public ICollection<Author> FindAuthorByName(string term)
        {
            // TODO: woops - pulling in all authors and filtering in application (not on database)
            // TODO: change this with a profiling view before and after to see the resulting query differences
            return AllAuthors.Where(_ => _.Name.ToLower().Contains(term.ToLower())).ToList();
        }

        public List<Author> AllAuthors
        {
            get { return GetEntities().ToList(); }
        }

        protected override DbSet<Author> GetEntities()
        {
            return _context.Authors;
        }

        protected override int GetId(Author entity)
        {
            return entity.Id;
        }
    }
}