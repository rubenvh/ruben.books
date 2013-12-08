using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruben.Books.Repository
{
    public class BooksFilter
    {
        public bool IsRead { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
    }
}
