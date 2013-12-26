using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ruben.Books.Web.Models
{
    public class BookVM
    {
        public BookVM()
        {
        }
        public int Id { get; set; }
        public string Title { get; set; }
       
        public DateTime? Published { get; set; }
        public int Pages { get; set; }

        public string FirstAuthor { get; set; }

        public string Category { get; set; }

        public int TimesRead { get; set; }

       
        public bool IsRead { get { return TimesRead > 0; } }
    }

    //public class BookDetailsVM : BookVM
    //{
    //    public DateTime? FirstPublished { get; set; }
    //    public string Isbn { get; set; }

    //    public ICollection<DateTime> Readings { get; set; }
    //}
}