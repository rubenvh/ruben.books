using System.Data.Entity;
using Ruben.Books.Domain;

namespace Ruben.Books.DataLayer
{
    public class BooksContext : DbContext
    {       
        public BooksContext()
            : base("BooksContext")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryGroup> CategoryGroups { get; set; }
        public DbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CategoryGroupConfiguration());
            modelBuilder.Configurations.Add(new AuthorConfiguration());
            modelBuilder.Configurations.Add(new ReadingConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}