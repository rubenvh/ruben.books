using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ruben.Books.Web.Models
{
    public class BookBadgeVM
    {
        public int Id { get; set; }

        public DateTime EarnedDate { get; set; }

        public string Book { get; set; }
        public int BookId { get; set; }
    }
}