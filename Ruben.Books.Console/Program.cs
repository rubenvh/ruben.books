using System;
using System.Linq;
using System.Data.Entity;
using Ruben.Books.Domain;
using Ruben.Books.Repository;
using System.Collections.Generic;
using Ruben.Books.CommandLine.OldDatabaseModel;

namespace Ruben.Books.CommandLine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new rubenvh_boekenEntities())
            using (var unitOfWork = new UnitOfWork())
            using (var booksRepo = new BooksRepository(unitOfWork))
            using (var categoriesRepo = new CategoryRepository(unitOfWork))
            using (var authorsRepo = new AuthorRepository(unitOfWork))
            {
                Console.WriteLine("Found {0} books.", context.boeken.Count());

                var categories = categoriesRepo.All.ToList();

                var allBooksQuery = context.boeken
                    .Include(_ => _.Readings)
                    .Include(_ => _.GenreLinks)
                    .Include(_ => _.GenreLinks.Select(g => g.Genre));
                var allBooks = allBooksQuery.ToList();

                var authorDictionary = PrefillAuthors(allBooks, authorsRepo, unitOfWork);
                var newBooks = new List<Book>(allBooks.Count);

                foreach (var book in allBooks)
                {
                    Console.WriteLine("Book {0} in {1}: {2}",
                        book.boekID,
                        book.GenreLinks.First().Genre.naam,
                        book.Readings.Any() ? book.Readings.First().datum.ToString() : "not read");

                    newBooks.Add(new Book()
                    {
                        State = State.Added,
                        FirstPublished = book.jaar.HasValue && book.jaar!=0? new DateTime(book.jaar.Value, 1, 1) : default(DateTime?),
                        Isbn = book.isbn,
                        Pages = book.blz ?? 0,
                        Title = book.titel,
                        Authors = new List<Author>() { authorsRepo.Find(authorDictionary[book.auteurs.ToLower()]) },
                        Readings = book.Readings.Select(_ => new Reading()
                                    {
                                        State = State.Added,
                                        PagesRead = book.blz ?? 0,
                                        Date = _.datum
                                    }).ToList(),
                        CategoryId = categories.Single(_ => _.Name == book.GenreLinks.First().Genre.naam).Id,
                        Tags = book.tags
                    });
                }

                newBooks.ForEach(booksRepo.InsertOrUpdateGraph);
                unitOfWork.Save();
            }
           
        }

        private static IDictionary<string, int> PrefillAuthors(IList<boeken> books, AuthorRepository authorsRepo, UnitOfWork unitOfWork)
        {
            var allAuthorsInOldDb = books.Select(_ => _.auteurs.Trim()).Distinct().ToList();
            var newAuthors = authorsRepo.All.ToList();
            var result =  new Dictionary<string, int>();
            Console.WriteLine("Found {0} authors in old db", allAuthorsInOldDb.Count());
            foreach(var a in allAuthorsInOldDb)
            {
                var candidate = newAuthors.FirstOrDefault(_ => _.Name.ToLower() == a.ToLower());

                if (candidate == null)
                {
                    candidate = new Author()
                    {
                        State = State.Added,
                        Name = a
                    };

                    authorsRepo.InsertOrUpdate(candidate);                    
                    unitOfWork.Save();
                    newAuthors.Add(candidate);
                }
                result[a.ToLower()] = candidate.Id;                
            }
            

            return result;
        }
    }
}