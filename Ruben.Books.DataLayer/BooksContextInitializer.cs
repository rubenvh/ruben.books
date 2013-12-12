using System.Collections.Generic;
using System.Data.Entity;
using Ruben.Books.Domain;

namespace Ruben.Books.DataLayer
{
    public class BooksContextInitializer : DropCreateDatabaseIfModelChanges<BooksContext>
    {
        protected override void Seed(BooksContext context)
        {
            base.Seed(context);

            var catNames = new List<string>()
            {
                "art",
                "comics",
                "computer",
                "culture",
                "economy",
                "fantasy",
                "french",
                "history",
                "literature",
                "nonfiction",
                "novel",
                "philosophy",
                "poetry",
                "psychology",
                "religion",
                "science",
                "science fiction",
                "_gepland"
            };

            catNames.ForEach(_ => context.Categories.Add(new Category() {Name = _}));

            context.Authors.Add(new Author() {Name = "Jos Borges"});
            context.Authors.Add(new Author() { Name = "Ruben VH" });
            context.SaveChanges();
            
            
        }
    }
}