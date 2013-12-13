using System.Data.Entity.ModelConfiguration;
using Ruben.Books.Domain;

namespace Ruben.Books.DataLayer
{
    public class  AuthorConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorConfiguration()
        {
            Property(_ => _.Name).IsRequired().HasMaxLength(100);
        }
    }
}