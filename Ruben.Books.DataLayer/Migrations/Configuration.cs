namespace Ruben.Books.DataLayer.Migrations
{
    using Ruben.Books.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ruben.Books.DataLayer.BooksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ruben.Books.DataLayer.BooksContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.CategoryGroups.AddOrUpdate(_ => _.Name,
                new CategoryGroup { Name = "art" },
                new CategoryGroup { Name = "fiction" },
                new CategoryGroup { Name = "nonfiction" },
                new CategoryGroup { Name = "computer" },
                new CategoryGroup { Name = "french" });
            context.SaveChanges();

            var groups = context.CategoryGroups.ToList();
            var art = groups.Single(_ => _.Name == "art");
            var fiction = groups.Single(_ => _.Name == "fiction");
            var nonfiction = groups.Single(_ => _.Name == "nonfiction");
            var computer = groups.Single(_ => _.Name == "computer");
            var french = groups.Single(_ => _.Name == "french");

            context.Categories.AddOrUpdate(_ => _.Name,
               new Category() { Name = "art", CategoryGroupId = art.Id },
               new Category() { Name = "comics", CategoryGroupId = fiction.Id },
               new Category() { Name = "computer", CategoryGroupId = computer.Id },
               new Category() { Name = "culture", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "economy", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "fantasy", CategoryGroupId = fiction.Id },
               new Category() { Name = "french", CategoryGroupId = french.Id },
               new Category() { Name = "history", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "literature", CategoryGroupId = fiction.Id },
               new Category() { Name = "nonfiction", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "novel", CategoryGroupId = fiction.Id },
               new Category() { Name = "philosophy", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "poetry", CategoryGroupId = fiction.Id },
               new Category() { Name = "psychology", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "religion", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "science", CategoryGroupId = nonfiction.Id },
               new Category() { Name = "science fiction", CategoryGroupId = fiction.Id },
               new Category() { Name = "_gepland" }
           );
            context.SaveChanges();
            
            base.Seed(context);

        }
    }
}
