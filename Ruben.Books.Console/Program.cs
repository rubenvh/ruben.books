using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruben.Books.DataLayer;
using Ruben.Books.Domain;

namespace Ruben.Books.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {

            Database.SetInitializer(new BooksContextInitializer());

            using (var context = new BooksContext())
            {
                Console.WriteLine("Available categories:");
                foreach (var a in context.Categories.ToList())
                {
                    Console.WriteLine(a.Name);
                }
                var category = context.Categories.OrderBy(_ => Guid.NewGuid()).Take(1).Single();


                Console.WriteLine();
                Console.WriteLine("Available authors");
                foreach (var a in context.Authors.ToList())
                {
                    Console.WriteLine(a.LastName);
                }

                var author = context.Authors.First();
                var newBook = new Book()
                {
                    Title = "Another book",
                    Pages = 100,
                    Category = category
                };
                newBook.Authors.Add(author);
                context.Books.Add(newBook);
                context.SaveChanges();

                Console.WriteLine();
                Console.WriteLine("Available books");
                foreach (var book in context.Books)
                {
                    Console.WriteLine("Book '{0}' in category {1}", book.Title, book.Category.Name);
                    
                }
            }
        }
    }
}
