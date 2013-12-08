using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Ruben.Books.Domain
{
    public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
            Readings = new List<Reading>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int FirstPublished { get; set; }
        public int Published { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }

        public ICollection<Author> Authors { get; set; }
        public Category Category { get; set; }
        public ICollection<Reading> Readings { get; set; }

        [NotMapped]
        public bool IsRead { get { return Readings.Any(); } }
    }
}