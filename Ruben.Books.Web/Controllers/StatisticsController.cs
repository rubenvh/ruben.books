using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ruben.Books.Repository;
using Ruben.Books.Web.Models;

namespace Ruben.Books.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private IStatisticsRepository _statisticsRepository;
        //
        // GET: /Statistics/
        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public ActionResult Index()
        {
            var vm = new StatsModelVM()
            {
                Averages = _statisticsRepository.GetGeneralAverages(),
                TopMonths = _statisticsRepository.GetTopMonths(5),
                AverageBooksPerYear = _statisticsRepository.GetAverageBooksPerYears(),
                AverageBooksPerCategory = _statisticsRepository.GetAveragePerCategory(),
                AverageBooksPerCategoryGroup = _statisticsRepository.GetAveragePerCategoryGroup(),
            };

            return View(vm);
        }

    }
}
