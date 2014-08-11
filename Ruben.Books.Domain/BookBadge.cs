using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruben.Books.Domain
{
    public class BookBadge : IObjectWithState
    {        
        public int Id { get; set; }
        public Reading Reading { get; set; }
        public int ReadingId { get; set; }
        public bool IsSpent { get; set; }

        [NotMapped]
        public State State { get; set; }
    }
}
