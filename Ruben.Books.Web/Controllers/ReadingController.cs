using Ruben.Books.DataLayer;
using Ruben.Books.Domain;
using Ruben.Books.Repository;
using Ruben.Books.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ruben.Books.Web.Controllers
{
    public class ReadingController : Controller
    {
         private IReadingsRepository _repo;
        private IUnitOfWork<BooksContext> _unitOfWork;

        public ReadingController(IReadingsRepository repo, IUnitOfWork<BooksContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        //
        // GET: /Books/

        public ActionResult Index()
        {
            // TODO: pass paging?
            var readings = _repo.GetTimeline();

            var viewModel = readings.Select(_ => new BookVM(_)).ToList();
            ViewBag.Title = "Timeline";
            return View("BookVMIndex", viewModel);
        }

        public ActionResult MarkRead()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult MarkRead(Reading reading)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(int id)
        {
            _repo.Delete(id);
            _unitOfWork.Save();
            return Index();
        }
    }
}