using System;

namespace Ruben.Books.Domain
{
    public class Reading
    {
        public int Id { get; set; }
        public Book Book{ get; set; }
        public DateTime Date { get; set; }
        public int PagesRead { get; set; }
    }
}