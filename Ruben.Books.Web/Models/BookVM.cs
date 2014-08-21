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
            Owned = book.Owned;
        }

        public BookVM(Reading reading): this(reading.Book)
        {
            LastRead = reading.Date;
        }

        public bool? Owned { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Published { get; set; }
        public int Pages { get; set; }

        [DisplayName("Author(s)")]
        public string Author { get; set; }

        public string Category { get; set; }

        public int TimesRead { get; set; }

         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastRead { get; set; }
               
        public bool IsRead { get { return TimesRead > 0; } }
    }

    public class CreateOrUpdateBookVM
    {
        public CreateOrUpdateBookVM(Book book)
        {
            Title = book.Title;
            YearFirstPublished = book.FirstPublished.HasValue? book.FirstPublished.Value.Year : 0;
            YearPublished = book.Published.HasValue ? book.Published.Value.Year : 0;
            Pages = book.Pages;
            AuthorIds = book.Authors.Select(_ => _.Id).ToArray();
            CategoryId = book.CategoryId;
            Tags = book.Tags;
            Isbn = book.Isbn;
            Owned = book.Owned.HasValue ? book.Owned.Value : false;
            Id = book.Id;
        }
        public CreateOrUpdateBookVM()
        {
            AuthorIds = new int[0];
        }
        [Required(ErrorMessage="Title is a required field.")]
        public string Title { get; set; }
        
        public int YearFirstPublished { get; set; }
        public int YearPublished { get; set; }
        public string Isbn { get; set; }

        [Required]
        public int Pages { get; set; }

        [Required]
        [MinLength(1)]
        public int[] AuthorIds { get; set; }

        [Required]
        public int CategoryId { get; set; }        
        public string Tags { get; set; }

        [Required]
        public bool Owned { get; set; }

        public int Id { get; set; }
    }

    //public class BookDetailsVM : BookVM
    //{
    //    public DateTime? FirstPublished { get; set; }
    //    public string Isbn { get; set; }

    //    public ICollection<DateTime> Readings { get; set; }
    //}
}