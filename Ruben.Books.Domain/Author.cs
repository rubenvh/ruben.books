using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Ruben.Books.Domain
{
    public class Author : IObjectWithState
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }

        [NotMapped]
        public State State { get; set; }
        
    }
}