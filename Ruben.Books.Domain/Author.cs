using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Ruben.Books.Domain
{
    public class Author : IObjectWithState
    {
        public Author()
        {
            
        }

        public Author(string id)
        {
            Id = int.Parse(id);
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        [NotMapped]
        public State State { get; set; }
        
    }
}