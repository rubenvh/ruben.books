using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ruben.Books.Web.Models
{
    public class BookFilterVM
    {        
        public bool? IsRead { get; set; }
        public int? CategoryId { get; set; }
    }
}