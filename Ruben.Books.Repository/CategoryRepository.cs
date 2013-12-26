using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ruben.Books.DataLayer;
using Ruben.Books.Domain;

namespace Ruben.Books.Repository
{
    public interface ICategoryRepository : IEntityRepository<Category> { }

    public class CategoryRepository : BaseEntityRepository<Category>, ICategoryRepository
    {
        private readonly BooksContext _context;

        public CategoryRepository(IUnitOfWork<BooksContext> uow)
            : base(uow.Context)
        {
            _context = uow.Context;
        }

        public List<Category> AllCategories
        {
            get { return GetEntities().ToList(); }
        }

        protected override DbSet<Category> GetEntities()
        {
            return _context.Categories;
        }

        protected override int GetId(Category entity)
        {
            return entity.Id;
        }
    }
}