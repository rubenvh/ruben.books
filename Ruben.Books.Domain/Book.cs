using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Ruben.Books.Domain
{
    public class Book : IObjectWithState
    {
        public Book()
        {
            Authors = new List<Author>();
            Readings = new List<Reading>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? FirstPublished { get; set; }
        public DateTime? Published { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }

        public ICollection<Author> Authors { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Reading> Readings { get; set; }

        [NotMapped]
        public bool IsRead { get { return Readings.Any(); } }

        [NotMapped]
        public State State { get; set; }
    }

    public interface IObjectWithState
    {
        State State { get; set; }
    }
    public enum State
    {        
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}