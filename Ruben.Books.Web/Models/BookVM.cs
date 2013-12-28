using Ruben.Books.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ruben.Books.Web.Models
{
    public class BookVM
    {
        public BookVM(Book book)
        {
            Title = book.Title;
            Published = book.FirstPublished ?? book.Published;
            TimesRead = book.Readings == null ? 0 : book.Readings.Count();
            Category = book.Category.Name;
            Author = string.Format(book.Authors.Count() == 1 ? "{0}" : "{0}, et al.", book.Authors.First().Name);
            Pages = book.Pages;
            Id = book.Id;
            LastRead = book.Readings != null && book.Readings.Any() ? book.Readings.First().Date : default(Nullable<DateTime>);

        }

        public BookVM(Reading reading): this(reading.Book)
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Published { get; set; }
        public int Pages { get; set; }

        [DisplayName("Author(s)")]
        public string Author { get; set; }

        public string Category { get; set; }

        public int TimesRead { get; set; }
        public DateTime? LastRead { get; set; }
               
        public bool IsRead { get { return TimesRead > 0; } }
    }

    //public class BookDetailsVM : BookVM
    //{
    //    public DateTime? FirstPublished { get; set; }
    //    public string Isbn { get; set; }

    //    public ICollection<DateTime> Readings { get; set; }
    //}
}