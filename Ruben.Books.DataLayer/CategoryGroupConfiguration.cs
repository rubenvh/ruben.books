using System.Data.Entity.ModelConfiguration;
using Ruben.Books.Domain;

namespace Ruben.Books.DataLayer
{
    public class CategoryGroupConfiguration : EntityTypeConfiguration<CategoryGroup>
    {
        public CategoryGroupConfiguration()
        {
            Property(_ => _.Name).IsRequired().HasMaxLength(25);
        }
    }
}