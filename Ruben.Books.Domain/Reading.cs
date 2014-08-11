using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruben.Books.Domain
{
    public class Reading : IObjectWithState
    {
        public Reading()
        {
            BadgesEarned = new List<BookBadge>();
        }

        public int Id { get; set; }
        public Book Book{ get; set; }
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public int PagesRead { get; set; }

        public virtual ICollection<BookBadge> BadgesEarned { get; set; }

        [NotMapped]
        public State State
        {
            get;
            set;
        }
    }
}