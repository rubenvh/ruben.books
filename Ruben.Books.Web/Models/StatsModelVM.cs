using Ruben.Books.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ruben.Books.Web.Models
{
    public class StatsModelVM
    {
        public IEnumerable<Tuple<DateTime,int>> TopMonths { get; set; }
        public IEnumerable<Tuple<int, ReadingAverage>> AverageBooksPerYear { get; set; }
        public IEnumerable<Tuple<string, ReadingAverage>> AverageBooksPerCategory { get; set; }
        public IEnumerable<Tuple<string, ReadingAverage>> AverageBooksPerCategoryGroup { get; set; }
        public Averages Averages { get; set; }
    }
}