using Ruben.Books.DataLayer;
using Ruben.Books.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruben.Books.Repository
{
    public interface IBookBadgeRepository : IEntityRepository<BookBadge>
    {

        void SpendBadgesForBook();
    }
    public class BookBadgeRepository : BaseEntityRepository<BookBadge>, IBookBadgeRepository
    {
        private readonly BooksContext _context;

        public BookBadgeRepository(IUnitOfWork<BooksContext> uow)
            : base(uow.Context)
        {
            _context = uow.Context;
        }


        protected override System.Data.Entity.DbSet<BookBadge> GetEntities()
        {
            return _context.EarnedBadges;
        }

        protected override int GetId(BookBadge entity)
        {
            return entity.Id;
        }

        public void SpendBadgesForBook()
        {
            var badgesToSpent = All.Where(_ => !_.IsSpent).Take(2).ToList();
            
            foreach (var b in badgesToSpent)
            {
                b.State = State.Modified;
                b.IsSpent = true;
            }
            badgesToSpent.ForEach(_=>InsertOrUpdate(_));
        }
    }
}
