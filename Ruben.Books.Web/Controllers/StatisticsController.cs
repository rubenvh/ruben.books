using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ruben.Books.Repository;

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
            var stats = _statisticsRepository.GetGeneralAverages();
            return View(stats);
        }

    }
}
