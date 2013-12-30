using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ruben.Books.Web.Models
{
    public class BookAndFilterVM
    {
        public IEnumerable<BookVM> Books { get; set; }
        public BookFilterVM Filter { get; set; }
        public SelectList CategoryIds { get; set; }
    }
}