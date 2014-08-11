using System.Data.Entity.ModelConfiguration;
using Ruben.Books.Domain;

namespace Ruben.Books.DataLayer
{
    public class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            Property(_ => _.Title).IsRequired();

        }
    }

    public class BookBadgeConfiguration : EntityTypeConfiguration<BookBadge>
    {
        public BookBadgeConfiguration()
        {
            Property(_ => _.IsSpent).IsRequired();
        }
    }
}