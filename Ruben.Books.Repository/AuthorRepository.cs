using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ruben.Books.DataLayer;
using Ruben.Books.Domain;

namespace Ruben.Books.Repository
{

    interface IAuthorRepository : IEntityRepository<Author> 
    {
        ICollection<Author> FindAuthorByName(string term);
    }

    public class AuthorRepository : BaseEntityRepository<Author>, IAuthorRepository
    {
        private readonly BooksContext _context;

        public AuthorRepository(UnitOfWork uow)
            : base(uow.Context)
        {
            _context = uow.Context;
        }

        public ICollection<Author> FindAuthorByName(string term)
        {
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