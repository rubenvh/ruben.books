using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ruben.Books.Repository
{
    public interface IStatisticsRepository
    {
        Averages GetGeneralAverages();
        IDictionary<DateTime, int> GetReadingsPerMonth();
        IDictionary<int, AverageBooksPerYear> GetAverageBooksPerYears();
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
                BooksPerMonth = 0,
                PagesPerYear = 0,
                PagesPerMonth = 0,
                PagesPerBook = _readingsRepository.All.Average(_=>_.Book.Pages),
            };
        }

        public IDictionary<DateTime, int> GetReadingsPerMonth()
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, AverageBooksPerYear> GetAverageBooksPerYears()
        {
            throw new NotImplementedException();
        }
    }

    public class Averages
    {
        public double BooksPerYear { get; set; }   
        public double BooksPerMonth { get; set; }
        public double PagesPerYear { get; set; }
        public double PagesPerMonth { get; set; }
        public double PagesPerBook { get; set; }
    }

    public class AverageBooksPerYear
    {
        public int BooksRead { get; set; }
        public int TotalPagesRead { get; set; }
        public double PagesPerDay { get; set; }
    }


}