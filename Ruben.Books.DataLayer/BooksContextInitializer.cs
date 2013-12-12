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
            var art = new CategoryGroup {Name="art"};
            var fiction = new CategoryGroup {Name="fiction"};
            var nonfiction = new CategoryGroup { Name = "nonfiction" };
            var computer = new CategoryGroup { Name = "computer" };
            var french = new CategoryGroup { Name = "french" };
            var catGroups = new List<CategoryGroup>() { art, fiction, nonfiction, computer, french };
            catGroups.ForEach(_=>context.CategoryGroups.Add(_));
            context.SaveChanges();

            var catNames = new List<Category>()
            {
                new Category() {Name ="art",CategoryGroupId=art.Id},
                new Category() {Name ="comics",CategoryGroupId=fiction.Id},
                new Category() {Name ="computer",CategoryGroupId=computer.Id},
                new Category() {Name ="culture",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="economy",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="fantasy",CategoryGroupId=fiction.Id},
                new Category() {Name ="french",CategoryGroupId=french.Id},
                new Category() {Name ="history",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="literature",CategoryGroupId=fiction.Id},
                new Category() {Name ="nonfiction",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="novel",CategoryGroupId=fiction.Id},
                new Category() {Name ="philosophy",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="poetry",CategoryGroupId=fiction.Id},
                new Category() {Name ="psychology",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="religion",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="science",CategoryGroupId=nonfiction.Id},
                new Category() {Name ="science fiction",CategoryGroupId=fiction.Id},
                new Category() {Name ="_gepland"}
            };

            catNames.ForEach(_ => context.Categories.Add(_));

            context.Authors.Add(new Author() {Name = "Jos Borges"});
            context.Authors.Add(new Author() { Name = "Ruben VH" });
            context.SaveChanges();
            
            
        }
    }
}