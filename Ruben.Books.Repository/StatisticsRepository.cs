using Ruben.Books.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ruben.Books.Repository
{
    public interface IStatisticsRepository
    {
        Averages GetGeneralAverages();
        IDictionary<DateTime, int> GetReadingsPerMonth();
        IEnumerable<Tuple<int, ReadingAverage>> GetAverageBooksPerYears();
        IEnumerable<Tuple<DateTime, int>> GetTopMonths(int topSize);
        IEnumerable<Tuple<string, ReadingAverage>> GetAveragePerCategory();
        IEnumerable<Tuple<string, ReadingAverage>> GetAveragePerCategoryGroup();
    }

    public class StatisticsRepository : IStatisticsRepository
    {
        private IReadingsRepository _readingsRepository;

        public StatisticsRepository(IReadingsRepository readingsRepository)
        {
            _readingsRepository = readingsRepository;
        }

        public Averages GetGeneralAverages()
        {
            return new Averages
            {
                BooksPerYear = _readingsRepository.All.GroupBy(_ => _.Date.Year).Average(_ => _.Count()),                
                PagesPerYear = _readingsRepository.All.GroupBy(_ => _.Date.Year).Average(_ => _.Sum(b=>b.PagesRead)),                
                PagesPerBook = _readingsRepository.All.Average(_=>_.Book.Pages),
                TotalPagesRead = _readingsRepository.All.Sum(_=>_.PagesRead),
                TotalBooksRead = _readingsRepository.All.Count(),
            };
        }

        public IDictionary<DateTime, int> GetReadingsPerMonth()
        {
            throw new NotImplementedException();
        }                
        
        public IEnumerable<Tuple<int, ReadingAverage>> GetAverageBooksPerYears()
        {
            return _readingsRepository.All.GroupBy(_ => _.Date.Year)
                .Select(_ => new { 
                    Year = _.Key,
                    A = new ReadingAverage
                    {
                        BooksRead = _.Count(),
                        TotalPagesRead = _.Sum(r => r.PagesRead),
                        PagesPerDay = _.Sum(r => r.PagesRead)/365.0
                    }
                })
                .ToList()
                .Select(_=>new Tuple<int,ReadingAverage>(_.Year, _.A))
                .ToList();
        }


        public IEnumerable<Tuple<DateTime, int>> GetTopMonths(int topSize)
        {
            return _readingsRepository.All
                .GroupBy(_ => new { _.Date.Year, _.Date.Month })
                .OrderByDescending(_ => _.Count())
                .Take(topSize)
                .ToList()
                .Select(_ => new Tuple<DateTime, int>(new DateTime(_.Key.Year, _.Key.Month, 1), _.Count()))
                .OrderByDescending(_=>_.Item2).ThenByDescending(_=>_.Item1)
                .ToList();
        }


        public IEnumerable<Tuple<string, ReadingAverage>> GetAveragePerCategory()
        {
            var firstDate = _readingsRepository.All.Min(_ => _.Date);

            return _readingsRepository.All
                .GroupBy(_ => _.Book.Category.Name)
                .Select(_ => new
                {
                    _.Key,
                    Avg = new ReadingAverage
                    {
                        BooksRead = _.Count(),
                        TotalPagesRead = _.Sum(r => r.PagesRead),                                                
                    }                                  
                })
                .ToList()
                
                .Select(_ => {
                    _.Avg.PagesPerDay = (double) _.Avg.TotalPagesRead / (double) DateTime.Now.Subtract(firstDate).Days;
                    return new Tuple<string, ReadingAverage>(_.Key, _.Avg);
                })
                .ToList();
        }

        public IEnumerable<Tuple<string, ReadingAverage>> GetAveragePerCategoryGroup()
        {
            var firstDate = _readingsRepository.All.Min(_ => _.Date);

            return _readingsRepository.All
                .GroupBy(_ => _.Book.Category.CategoryGroup.Name)
                .Select(_ => new
                {
                    _.Key,
                    Avg = new ReadingAverage
                    {
                        BooksRead = _.Count(),
                        TotalPagesRead = _.Sum(r => r.PagesRead),
                    }
                })
                .ToList()

                .Select(_ =>
                {
                    _.Avg.PagesPerDay = (double)_.Avg.TotalPagesRead / (double)DateTime.Now.Subtract(firstDate).Days;
                    return new Tuple<string, ReadingAverage>(_.Key, _.Avg);
                })
                .ToList();
        }       
    }

    public class Averages
    {
        [DisplayFormat(DataFormatString = "{0:#,##0.0#}")]
        public double BooksPerYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0#}")]
        public double BooksPerMonth { get { return BooksPerYear / 12.0; } }

        [DisplayFormat(DataFormatString = "{0:#,##0.0#}")]
        public double PagesPerYear { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0#}")]
        public double PagesPerMonth { get { return PagesPerYear / 12.0; } }

        [DisplayFormat(DataFormatString = "{0:#,##0.0#}")]
        public double PagesPerBook { get; set; }

        public int TotalPagesRead { get; set; }

        public int TotalBooksRead { get; set; }
    }

    public class ReadingAverage
    {
        public int BooksRead { get; set; }
        public int TotalPagesRead { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0#}")]
        public double PagesPerDay { get; set; }       
    }


}