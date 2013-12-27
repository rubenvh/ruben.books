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
                foreach (Category a in repo.AllIncluding(_ => _.CategoryGroup))
                {
                    Console.WriteLine("{0} => {1}", a.Name, (a.CategoryGroup!=null)? a.CategoryGroup.Name : "No group");
                }
                category = repo.All.OrderBy(_ => Guid.NewGuid()).Take(1).Single();
            }
            using (var repo = new AuthorRepository(new UnitOfWork()))
            {
                Console.WriteLine();
                Console.WriteLine("Available authors");
                foreach (Author a in repo.AllAuthors)
                {
                    Console.WriteLine(a.Name);
                }
                author = repo.All.First();

                 Console.WriteLine();
                Console.WriteLine("All authors matching 'bor':");
                foreach (Author a in repo.FindAuthorByName("bor"))
                {
                    Console.WriteLine(a.Name);
                }   
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
                Random rnd = new Random(System.DateTime.Now.Millisecond);
                Book randomBook = repo.All.OrderBy(_ => Guid.NewGuid()).Take(1).Single();                
                int month = rnd.Next(1, 13); // creates a number between 1 and 12
                repo.MarkAsRead(randomBook.Id, DateTime.Now.Date.AddMonths(-1*month));
                uow.Save();
            }

            using (var uow = new UnitOfWork())
            using (var repo = new BooksRepository(uow))
            {
                Console.WriteLine();
                Console.WriteLine("Available books");
                foreach (Book book in repo.AllIncluding(_ => _.Category, _ => _.Authors, _ => _.Readings))
                {
                    Console.WriteLine("Book '{0}' in category {1}; read {2} times.", book.Title, book.Category.Name, book.Readings.Count());
                }
            }
        }
    }
}