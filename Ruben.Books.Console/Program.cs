using System;
using System.Linq;
using Ruben.Books.Domain;
using Ruben.Books.Repository;

namespace Ruben.Books.CommandLine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Category category;
            Author author;

            using (var repo = new CategoryRepository(new UnitOfWork()))
            {
                Console.WriteLine("Available categories:");
                foreach (Category a in repo.AllCategories)
                {
                    Console.WriteLine(a.Name);
                }
                category = repo.All.OrderBy(_ => Guid.NewGuid()).Take(1).Single();
            }
            using (var repo = new AuthorRepository(new UnitOfWork()))
            {
                Console.WriteLine();
                Console.WriteLine("Available authors");
                foreach (Author a in repo.AllAuthors)
                {
                    Console.WriteLine(a.LastName);
                }
                author = repo.All.First();
            }

            using (var uow = new UnitOfWork())
            using (var repo = new BooksRepository(uow))
            {
                var newBook = new Book
                {
                    Title = "Another book",
                    Pages = 100,
                    Category = category
                };
                newBook.Authors.Add(author);
                newBook.State = State.Added;
                repo.InsertOrUpdateGraph(newBook);
                
                uow.Save();
            }

            using (var uow = new UnitOfWork())
            using (var repo = new BooksRepository(uow))
            {
                Console.WriteLine();
                Console.WriteLine("Available books");
                foreach (Book book in repo.AllIncluding(_ => _.Category, _ => _.Authors))
                {
                    Console.WriteLine("Book '{0}' in category {1}", book.Title, book.Category.Name);
                }
            }
        }
    }
}