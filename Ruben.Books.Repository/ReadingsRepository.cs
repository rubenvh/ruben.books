using Ruben.Books.DataLayer;
using Ruben.Books.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Ruben.Books.Repository
{
    public interface IReadingsRepository : IEntityRepository<Reading>
    {
        IList<Reading> GetTimeline();
    }

    public class ReadingsRepository : BaseEntityRepository<Reading>, IReadingsRepository
    {
        private readonly BooksContext _context;

        public ReadingsRepository(IUnitOfWork<BooksContext> uow)
            : base(uow.Context)
        {
            _context = uow.Context;
        }

        protected override System.Data.Entity.DbSet<Reading> GetEntities()
        {
            return _context.Readings;
        }

        protected override int GetId(Reading entity)
        {
            return entity.Id;
        }

        public IList<Reading> GetTimeline()
        {
            return GetEntities()
                .Include(_ => _.Book)
                .Include(_ => _.Book.Authors)
                .Include(_ => _.Book.Category)
                .OrderByDescending(_ => _.Date)
                .ToList();
        }
    }
}
