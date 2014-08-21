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
        void CreateBadgesForBookRead(Reading book);
        void SpendBadge(int bookBadgeId);
    }
    public class BookBadgeRepository : BaseEntityRepository<BookBadge>, IBookBadgeRepository
    {
        private readonly BooksContext _context;
        private readonly IStatisticsRepository _stats;

        public BookBadgeRepository(IUnitOfWork<BooksContext> uow, IStatisticsRepository statisticsRepo)
            : base(uow.Context)
        {
            _context = uow.Context;
            _stats = statisticsRepo;
        }


        protected override System.Data.Entity.DbSet<BookBadge> GetEntities()
        {
            return _context.EarnedBadges;
        }

        protected override int GetId(BookBadge entity)
        {
            return entity.Id;
        }

        public void SpendBadge(int bookBadgeId)
        {
            var badgeToSpend = Find(bookBadgeId);
            badgeToSpend.State = State.Modified;
            badgeToSpend.IsSpent = true;
            InsertOrUpdate(badgeToSpend);
        }

        public void CreateBadgesForBookRead(Reading reading)
        {
            if (reading.Book.Owned.HasValue && reading.Book.Owned.Value)
            {
                int avgPages = (int) Math.Round(_stats.GetGeneralAverages().PagesPerBook);
                int badgesEarned = Math.Max(1, reading.PagesRead / avgPages);
                
                // earning badge(s)
                for (int i = 0; i < badgesEarned; i++)
                { 
                    reading.BadgesEarned.Add(new BookBadge()
                    {
                        State = State.Added,
                        Reading = reading,
                        IsSpent = false,
                    });
                }
            }
        }
    }
}
